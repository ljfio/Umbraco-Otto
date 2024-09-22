// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Our.Umbraco.Organizers.Core;
using Our.Umbraco.Organizers.Core.Config;
using Our.Umbraco.Organizers.Core.Engines;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Organizers;

public class Organizer : IOrganizer
{
    private readonly OrganizerEngineCollection _engineCollection;
    private readonly IServiceProvider _serviceProvider;
    private readonly IEntityService _entityService;
    private readonly IMediaService _mediaService;
    private readonly IContentService _contentService;
    private readonly IOptions<OrganizerSettings> _options;

    private readonly IDictionary<string, IOrganizerEngine> _engines;

    public Organizer(
        OrganizerEngineCollection engineCollection,
        IServiceProvider serviceProvider,
        IEntityService entityService,
        IOptions<OrganizerSettings> options, IContentService contentService, IMediaService mediaService)
    {
        _engineCollection = engineCollection;
        _options = options;
        _contentService = contentService;
        _mediaService = mediaService;
        _entityService = entityService;
        _serviceProvider = serviceProvider;

        _engines = new Dictionary<string, IOrganizerEngine>();
    }

    public void Organize(IEnumerable<IContentBase> entities)
    {
        var ruleGroups = entities
            .GroupBy(FindMatchingRule)
            .Where(group => group.Key is not null);

        foreach (var grouping in ruleGroups)
        {
            var rule = grouping.Key;

            if (rule is null)
                continue;

            var engine = GetEngine(rule.Engine);

            if (engine is null)
                continue;

            engine.Organize(rule, grouping.ToArray());
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

    private IOrganizerEngineRule? FindMatchingRule(IContentBase entity)
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

    private IOrganizerEngineRule? FindMatchingRule(
        IEnumerable<IOrganizerEngineRule> rules,
        IContentBase entity,
        IContentBase parent) =>
        rules.FirstOrDefault(rule => rule.Matches(entity, parent));

    private IOrganizerEngine? GetEngine(string name)
    {
        // Search for engine in the cache
        if (_engines.TryGetValue(name, out var cachedEngine))
            return cachedEngine;

        // Locate the engine based on the name
        var engineType = _engineCollection.FindEngineType(name);

        if (engineType is null)
            return null;

        // Activate engine using the service provider
        var engine = ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, engineType) as IOrganizerEngine;

        if (engine is null)
            return null;

        // Add the engine to the cache
        _engines.Add(name, engine);

        return engine;
    }
}