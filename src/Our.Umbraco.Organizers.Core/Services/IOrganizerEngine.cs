// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Config;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Organizers.Core.Engines;

public interface IOrganizerEngine<in TRule, in TEntity> : IOrganizerEngine<TEntity>
    where TRule : class, IOrganizerEngineRule
    where TEntity: class, IContentBase
{
    /// <summary>
    /// Organise the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    OperationResult Organize(TRule rule, TEntity[] entities);

    /// <inheritdoc />
    OperationResult IOrganizerEngine<TEntity>.Organize(IOrganizerEngineRule rule, TEntity[] entities)
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
    OperationResult IOrganizerEngine<TEntity>.Cleanup(IOrganizerEngineRule rule, TEntity[] entities)
    {
        if (rule is not TRule typedRule)
            throw new ArgumentException($"must be of type {typeof(TRule).Name}", nameof(rule));
        
        return Cleanup(typedRule, entities);
    }
}

public interface IOrganizerEngine<in TEntity>
    where TEntity : class, IContentBase
{
    /// <summary>
    /// Organise the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    OperationResult Organize(IOrganizerEngineRule rule, TEntity[] entities);

    /// <summary>
    /// Cleanup the folders linked to the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    OperationResult Cleanup(IOrganizerEngineRule rule, TEntity[] entities);
}