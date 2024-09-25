// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Our.Umbraco.Otto.Core;
using Our.Umbraco.Otto.Core.Organizers;
using Our.Umbraco.Otto.Core.Rules;
using Our.Umbraco.Otto.Core.Selectors;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using MatchType = Our.Umbraco.Otto.Core.MatchType;

namespace Our.Umbraco.Otto.Selectors;

public abstract class OrganizerSelectorBase<TEntity> : IOrganizerSelector<TEntity>
    where TEntity : class, IContentBase
{
    private readonly OrganizerCollection<TEntity> _collection;
    private readonly IServiceProvider _serviceProvider;

    private readonly IDictionary<string, IOrganizer<TEntity>> _organizers;

    public OrganizerSelectorBase(
        OrganizerCollection<TEntity> collection,
        IServiceProvider serviceProvider)
    {
        _collection = collection;
        _serviceProvider = serviceProvider;

        _organizers = new Dictionary<string, IOrganizer<TEntity>>();
    }

    public void Organize(IEnumerable<TEntity> entities, OrganizerMode mode)
    {
        var ruleGroups = GroupByRule(entities, mode);

        foreach (var grouping in ruleGroups)
        {
            var rule = grouping.Key;

            var strategy = GetStrategy(rule.Organizer);

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
        IOrganizer<TEntity> strategy,
        IOrganizerRule rule,
        IEnumerable<Match<TEntity>> matches,
        OrganizerMode mode) => mode switch
    {
        OrganizerMode.Organize => strategy.Organize(rule, matches),
        OrganizerMode.Cleanup => strategy.Cleanup(rule, matches),
        _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null),
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

    private IOrganizer<TEntity>? GetStrategy(string name)
    {
        // Search for strategy in the cache
        if (_organizers.TryGetValue(name, out var cachedStrategy))
            return cachedStrategy;

        // Locate the strategy based on the name
        var strategyType = _collection.FindOrganizerByName(name);

        if (strategyType is null)
            return null;

        // Activate strategy using the service provider
        var strategy = ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, strategyType)
            as IOrganizer<TEntity>;

        if (strategy is null)
            return null;

        // Add the strategy to the cache
        _organizers.Add(name, strategy);

        return strategy;
    }
}