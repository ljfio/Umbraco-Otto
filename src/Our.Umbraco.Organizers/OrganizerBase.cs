// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Our.Umbraco.Organizers.Core;
using Our.Umbraco.Organizers.Core.Rules;
using Our.Umbraco.Organizers.Core.Strategies;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using MatchType = Our.Umbraco.Organizers.Core.Rules.MatchType;

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
        var ruleGroups = GroupByRule(entities, mode);

        foreach (var grouping in ruleGroups)
        {
            var rule = grouping.Key;

            var strategy = GetStrategy(rule.Strategy);

            if (strategy is null)
                continue;

            var result = RunStrategy(strategy, rule, grouping.Value, mode);
        }
    }

    private IDictionary<IOrganizerRule, ICollection<Match<TEntity>>> GroupByRule(
        IEnumerable<TEntity> entities,
        OrganizerMode mode)
    {
        var groups = new Dictionary<IOrganizerRule, ICollection<Match<TEntity>>>();

        foreach (var entity in entities)
        {
            if (TryFindMatchingRule(entity, mode, out var rule, out var matchType))
            {
                var match = new Match<TEntity>(entity, matchType);
            
                if (groups.TryGetValue(rule, out var matches)) 
                    matches.Add(match);
                else
                    groups.Add(rule, new List<Match<TEntity>>([match]));
            }
        }

        return groups;
    }

    private OperationResult RunStrategy(
        IOrganizerStrategy<TEntity> strategy,
        IOrganizerRule rule,
        IEnumerable<Match<TEntity>> matches,
        OrganizerMode mode) => mode switch
    {
        OrganizerMode.Organize => strategy.Organize(rule, matches),
        OrganizerMode.Cleanup => strategy.Cleanup(rule, matches),
    };

    private bool TryFindMatchingRule(
        TEntity entity,
        OrganizerMode mode,
        [NotNullWhen(true)] out IOrganizerRule? rule,
        out MatchType matchType)
    {
        rule = null;
        matchType = MatchType.None;
        
        // Locate the parent and its type
        var parent = GetParent(entity);

        if (parent is null)
            return false;

        // Find the matching rule for either content or media
        foreach (var checkingRule in GetRules())
        {
            var checkingMatchType = checkingRule.Matches(entity, parent, mode);

            if (checkingMatchType != MatchType.None)
            {
                rule = checkingRule;
                matchType = checkingMatchType;
                
                return true;
            }
        }

        return false;
    }

    protected abstract IEnumerable<IOrganizerRule> GetRules();

    protected abstract TEntity? GetParent(TEntity entity);

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