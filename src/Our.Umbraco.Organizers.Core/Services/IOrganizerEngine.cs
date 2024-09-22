// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Config;
using Umbraco.Cms.Core.Models;

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
    void Organize(TRule rule, TEntity[] entities);

    /// <inheritdoc />
    void IOrganizerEngine<TEntity>.Organize(IOrganizerEngineRule rule, TEntity[] entities)
    {
        if (rule is not TRule typedRule)
            throw new ArgumentException($"must be of type {typeof(TRule).Name}", nameof(rule));
        
        Organize(typedRule, entities);
    }

    /// <summary>
    /// Cleanup the folders linked to the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    void Cleanup(TRule rule, TEntity[] entities);

    /// <inheritdoc />
    void IOrganizerEngine<TEntity>.Cleanup(IOrganizerEngineRule rule, TEntity[] entities)
    {
        if (rule is not TRule typedRule)
            throw new ArgumentException($"must be of type {typeof(TRule).Name}", nameof(rule));
        
        Cleanup(typedRule, entities);
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
    void Organize(IOrganizerEngineRule rule, TEntity[] entities);

    /// <summary>
    /// Cleanup the folders linked to the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    void Cleanup(IOrganizerEngineRule rule, TEntity[] entities);
}