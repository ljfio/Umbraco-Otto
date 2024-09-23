// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Rules;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Organizers.Core.Strategies;

public interface IOrganizerStrategy<in TRule, TEntity> : IOrganizerStrategy<TEntity>
    where TRule : class, IOrganizerRule
    where TEntity: class, IContentBase
{
    /// <summary>
    /// Organise the <paramref name="matches"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="matches"></param>
    OperationResult Organize(TRule rule, IEnumerable<Match<TEntity>> matches);

    /// <inheritdoc />
    OperationResult IOrganizerStrategy<TEntity>.Organize(IOrganizerRule rule, IEnumerable<Match<TEntity>> matches)
    {
        if (rule is not TRule typedRule)
            throw new ArgumentException($"must be of type {typeof(TRule).Name}", nameof(rule));
        
        return Organize(typedRule, matches);
    }

    /// <summary>
    /// Cleanup the folders linked to the <paramref name="matches"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="matches"></param>
    OperationResult Cleanup(TRule rule, IEnumerable<Match<TEntity>> matches);

    /// <inheritdoc />
    OperationResult IOrganizerStrategy<TEntity>.Cleanup(IOrganizerRule rule, IEnumerable<Match<TEntity>> matches)
    {
        if (rule is not TRule typedRule)
            throw new ArgumentException($"must be of type {typeof(TRule).Name}", nameof(rule));
        
        return Cleanup(typedRule, matches);
    }
}

public interface IOrganizerStrategy<TEntity>
    where TEntity : class, IContentBase
{
    /// <summary>
    /// Organise the <paramref name="matches"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="matches"></param>
    OperationResult Organize(IOrganizerRule rule, IEnumerable<Match<TEntity>> matches);

    /// <summary>
    /// Cleanup the folders linked to the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    OperationResult Cleanup(IOrganizerRule rule, IEnumerable<Match<TEntity>> entities);
}