// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Our.Umbraco.AutoFolders.Core.Config;
using Our.Umbraco.AutoFolders.Core.FolderEngine;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.AutoFolders.FolderEngine;

public class FolderEngineDispatcher : IFolderEngineDispatcher
{
    private readonly FolderEngineCollection _engineCollection;
    private readonly IServiceProvider _serviceProvider;
    private readonly IDictionary<string, IFolderEngine> _engines;
    private readonly IOptions<FolderSettings> _options;

    public FolderEngineDispatcher(
        FolderEngineCollection engineCollection,
        IServiceProvider serviceProvider,
        IOptions<FolderSettings> options)
    {
        _engineCollection = engineCollection;
        _options = options;
        _serviceProvider = serviceProvider;

        _engines = new Dictionary<string, IFolderEngine>();
    }

    public void Organise(IContentBase[] entities)
    {
        var ruleGroups = entities
            .GroupBy(FindMatchingRule);
        
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

    public void Cleanup(IContentBase[] entities)
    {
        var ruleGroups = entities
            .GroupBy(FindMatchingRule);
        
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
        
        if (entity is IContent content)
        {
        }
        
        if (entity is IMedia media)
        {
            
        }

        return null;
    }

    private IFolderEngine? GetEngine(string name)
    {
        // Search for engine
        if (_engines.TryGetValue(name, out var cachedEngine))
            return cachedEngine;

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