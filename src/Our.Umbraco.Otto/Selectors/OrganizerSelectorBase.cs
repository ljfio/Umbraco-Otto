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

    private readonly IDictionary<string, IOrganizer<TEntity>> _organizerCache;

    public OrganizerSelectorBase(
        OrganizerCollection<TEntity> collection,
        IServiceProvider serviceProvider)
    {
        _collection = collection;
        _serviceProvider = serviceProvider;

        _organizerCache = new Dictionary<string, IOrganizer<TEntity>>();
    }

    public void Organize(IEnumerable<TEntity> entities, OrganizerMode mode)
    {
        var ruleGroups = GroupByRule(entities, mode);

        foreach (var grouping in ruleGroups)
        {
            var rule = grouping.Key;

            var organizer = GetOrganizer(rule.Organizer);

            if (organizer is null)
                continue;

            var result = RunOrganizer(organizer, rule, grouping.Value, mode);
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

    private OperationResult RunOrganizer(
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

    private IOrganizer<TEntity>? GetOrganizer(string name)
    {
        // Search for organizer in the cache
        if (_organizerCache.TryGetValue(name, out var cached))
            return cached;

        // Locate the organizer based on the name
        var type = _collection.FindOrganizerByName(name);

        if (type is null)
            return null;

        // Activate organizer using the service provider
        var organizer = ActivatorUtilities.GetServiceOrCreateInstance(_serviceProvider, type)
            as IOrganizer<TEntity>;

        if (organizer is null)
            return null;

        // Add the organizer to the cache
        _organizerCache.Add(name, organizer);

        return organizer;
    }
}