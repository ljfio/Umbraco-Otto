// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.Extensions.DependencyInjection;
using Our.Umbraco.Organizers.Core;
using Our.Umbraco.Organizers.Core.Config;
using Our.Umbraco.Organizers.Core.Engines;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers;

public abstract class OrganizerBase<TEntity> : IOrganizer<TEntity>
    where TEntity : class, IContentBase
{
    private readonly OrganizerEngineCollection<TEntity> _engineCollection;
    private readonly IServiceProvider _serviceProvider;

    private readonly IDictionary<string, IOrganizerEngine<TEntity>> _engines;

    public OrganizerBase(
        OrganizerEngineCollection<TEntity> engineCollection,
        IServiceProvider serviceProvider)
    {
        _engineCollection = engineCollection;
        _serviceProvider = serviceProvider;

        _engines = new Dictionary<string, IOrganizerEngine<TEntity>>();
    }

    public void Organize(IEnumerable<TEntity> entities)
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

    public void Cleanup(IEnumerable<TEntity> entities)
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

    private IOrganizerEngineRule? FindMatchingRule(TEntity entity)
    {
        // Locate the parent and its type
        var parent = GetParent(entity);

        if (parent is null)
            return null;

        // Find the matching rule for either content or media
        return FindMatchingRule(GetRules(), entity, parent);
    }

    protected abstract IEnumerable<IOrganizerEngineRule> GetRules();
    
    protected abstract TEntity? GetParent(TEntity entity);

    private IOrganizerEngineRule? FindMatchingRule(
        IEnumerable<IOrganizerEngineRule> rules,
        IContentBase entity,
        IContentBase parent) =>
        rules.FirstOrDefault(rule => rule.Matches(entity, parent));

    private IOrganizerEngine<TEntity>? GetEngine(string name)
    {
        // Search for engine in the cache
        if (_engines.TryGetValue(name, out var cachedEngine))
            return cachedEngine;

        // Locate the engine based on the name
        var engineType = _engineCollection.FindEngineType(name);

        if (engineType is null)
            return null;

        // Activate engine using the service provider
        var engine = ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, engineType) as IOrganizerEngine<TEntity>;

        if (engine is null)
            return null;

        // Add the engine to the cache
        _engines.Add(name, engine);

        return engine;
    }
}