// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.Extensions.DependencyInjection;
using Our.Umbraco.Organizers.Core;
using Our.Umbraco.Organizers.Core.Config;
using Our.Umbraco.Organizers.Core.Rules;
using Our.Umbraco.Organizers.Core.Strategies;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Organizers;

public abstract class OrganizerBase<TEntity> : IOrganizer<TEntity>
    where TEntity : class, IContentBase
{
    private readonly OrganizerStrategyCollection<TEntity> _strategyCollection;
    private readonly IServiceProvider _serviceProvider;

    private readonly IDictionary<string, IOrganizerStrategy<TEntity>> _strategies;

    public OrganizerBase(
        OrganizerStrategyCollection<TEntity> strategyCollection,
        IServiceProvider serviceProvider)
    {
        _strategyCollection = strategyCollection;
        _serviceProvider = serviceProvider;

        _strategies = new Dictionary<string, IOrganizerStrategy<TEntity>>();
    }

    public void Organize(IEnumerable<TEntity> entities, OrganizerMode mode)
    {
        var ruleGroups = entities
            .GroupBy(entity => FindMatchingRule(entity, mode))
            .Where(group => group.Key is not null);

        foreach (var grouping in ruleGroups)
        {
            var rule = grouping.Key;

            if (rule is null)
                continue;

            var strategy = GetStrategy(rule.Strategy);

            if (strategy is null)
                continue;

            var result = RunStrategy(strategy, rule, grouping.ToArray(), mode);
        }
    }

    private OperationResult RunStrategy(
        IOrganizerStrategy<TEntity> strategy,
        IOrganizerRule rule,
        TEntity[] entities,
        OrganizerMode mode) => mode switch
    {
        OrganizerMode.Organize => strategy.Organize(rule, entities),
        OrganizerMode.Cleanup => strategy.Cleanup(rule, entities),
    };

    private IOrganizerRule? FindMatchingRule(TEntity entity, OrganizerMode mode)
    {
        // Locate the parent and its type
        var parent = GetParent(entity);

        if (parent is null)
            return null;

        // Find the matching rule for either content or media
        return FindMatchingRule(GetRules(), entity, parent, mode);
    }

    protected abstract IEnumerable<IOrganizerRule> GetRules();

    protected abstract TEntity? GetParent(TEntity entity);

    private IOrganizerRule? FindMatchingRule(
        IEnumerable<IOrganizerRule> rules,
        IContentBase entity,
        IContentBase parent,
        OrganizerMode mode) =>
        rules.FirstOrDefault(rule => rule.Matches(entity, parent, mode));

    private IOrganizerStrategy<TEntity>? GetStrategy(string name)
    {
        // Search for strategy in the cache
        if (_strategies.TryGetValue(name, out var cachedStrategy))
            return cachedStrategy;

        // Locate the strategy based on the name
        var strategyType = _strategyCollection.FindStrategyType(name);

        if (strategyType is null)
            return null;

        // Activate strategy using the service provider
        var strategy = ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, strategyType)
            as IOrganizerStrategy<TEntity>;

        if (strategy is null)
            return null;

        // Add the strategy to the cache
        _strategies.Add(name, strategy);

        return strategy;
    }
}