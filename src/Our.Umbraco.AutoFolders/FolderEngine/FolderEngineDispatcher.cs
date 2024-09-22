// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Our.Umbraco.AutoFolders.Core.Config;
using Our.Umbraco.AutoFolders.Core.FolderEngine;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Entities;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.AutoFolders.FolderEngine;

public class FolderEngineDispatcher : IFolderEngineDispatcher
{
    private readonly FolderEngineCollection _engineCollection;
    private readonly IServiceProvider _serviceProvider;
    private readonly IEntityService _entityService;
    private readonly IMediaService _mediaService;
    private readonly IContentService _contentService;
    private readonly IOptions<FolderSettings> _options;

    private readonly IDictionary<string, IFolderEngine> _engines;

    public FolderEngineDispatcher(
        FolderEngineCollection engineCollection,
        IServiceProvider serviceProvider,
        IEntityService entityService,
        IOptions<FolderSettings> options, IContentService contentService, IMediaService mediaService)
    {
        _engineCollection = engineCollection;
        _options = options;
        _contentService = contentService;
        _mediaService = mediaService;
        _entityService = entityService;
        _serviceProvider = serviceProvider;

        _engines = new Dictionary<string, IFolderEngine>();
    }

    public void Organise(IEnumerable<IContentBase> entities)
    {
        var ruleGroups = entities.GroupBy(FindMatchingRule);

        foreach (var grouping in ruleGroups)
        {
            var rule = grouping.Key;

            if (rule is null)
                continue;

            var engine = GetEngine(rule.Engine);

            if (engine is null)
                continue;

            engine.Organise(rule, grouping.ToArray());
        }
    }

    public void Cleanup(IEnumerable<IContentBase> entities)
    {
        var ruleGroups = entities.GroupBy(FindMatchingRule);

        foreach (var grouping in ruleGroups)
        {
            var rule = grouping.Key;

            if (rule is null)
                continue;

            var engine = GetEngine(rule.Engine);

            if (engine is null)
                continue;

            engine.Cleanup(rule, grouping.ToArray());
        }
    }

    private IFolderEngineRule? FindMatchingRule(IContentBase entity)
    {
        var settings = _options.Value;

        // Locate the parent and its type
        var parent = GetParent(entity);

        if (parent is null)
            return null;

        // Find the matching rule for either content or media
        if (entity is IContent)
            return FindMatchingRule(settings.Content.Rules, entity, parent);

        if (entity is IMedia)
            return FindMatchingRule(settings.Media.Rules, entity, parent);

        return null;
    }

    private IContentBase? GetParent(IContentBase entity) => entity switch
    {
        IMedia media => _mediaService.GetParent(media),
        IContent content => _contentService.GetParent(content),
    };

    private IFolderEngineRule? FindMatchingRule(
        IEnumerable<IFolderEngineRule> rules,
        IContentBase entity,
        IContentBase parent) =>
        rules.FirstOrDefault(rule => rule.Matches(entity, parent));

    private IFolderEngine? GetEngine(string name)
    {
        // Search for engine in the cache
        if (_engines.TryGetValue(name, out var cachedEngine))
            return cachedEngine;

        // Locate the engine based on the name
        var engineType = _engineCollection.FindEngineType(name);

        if (engineType is null)
            return null;

        // Activate engine using the service provider
        var engine = ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, engineType) as IFolderEngine;

        if (engine is null)
            return null;

        // Add the engine to the cache
        _engines.Add(name, engine);

        return engine;
    }
}