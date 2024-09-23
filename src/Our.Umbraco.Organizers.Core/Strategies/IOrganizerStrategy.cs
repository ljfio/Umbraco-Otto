// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Rules;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Organizers.Core.Strategies;

public interface IOrganizerStrategy<in TRule, in TEntity> : IOrganizerStrategy<TEntity>
    where TRule : class, IOrganizerRule
    where TEntity: class, IContentBase
{
    /// <summary>
    /// Organise the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    OperationResult Organize(TRule rule, TEntity[] entities);

    /// <inheritdoc />
    OperationResult IOrganizerStrategy<TEntity>.Organize(IOrganizerRule rule, TEntity[] entities)
    {
        if (rule is not TRule typedRule)
            throw new ArgumentException($"must be of type {typeof(TRule).Name}", nameof(rule));
        
        return Organize(typedRule, entities);
    }

    /// <summary>
    /// Cleanup the folders linked to the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    OperationResult Cleanup(TRule rule, TEntity[] entities);

    /// <inheritdoc />
    OperationResult IOrganizerStrategy<TEntity>.Cleanup(IOrganizerRule rule, TEntity[] entities)
    {
        if (rule is not TRule typedRule)
            throw new ArgumentException($"must be of type {typeof(TRule).Name}", nameof(rule));
        
        return Cleanup(typedRule, entities);
    }
}

public interface IOrganizerStrategy<in TEntity>
    where TEntity : class, IContentBase
{
    /// <summary>
    /// Organise the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    OperationResult Organize(IOrganizerRule rule, TEntity[] entities);

    /// <summary>
    /// Cleanup the folders linked to the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    OperationResult Cleanup(IOrganizerRule rule, TEntity[] entities);
}