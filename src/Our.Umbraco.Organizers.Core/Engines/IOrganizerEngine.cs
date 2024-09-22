// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Config;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core.Engines;

public interface IOrganizerEngine<in TRule> : IOrganizerEngine
    where TRule : class, IOrganizerEngineRule
{
    /// <summary>
    /// Organise the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    void Organize(TRule rule, IContentBase[] entities);

    /// <inheritdoc />
    void IOrganizerEngine.Organize(IOrganizerEngineRule rule, IContentBase[] entities)
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
    void Cleanup(TRule rule, IContentBase[] entities);

    /// <inheritdoc />
    void IOrganizerEngine.Cleanup(IOrganizerEngineRule rule, IContentBase[] entities)
    {
        if (rule is not TRule typedRule)
            throw new ArgumentException($"must be of type {typeof(TRule).Name}", nameof(rule));
        
        Cleanup(typedRule, entities);
    }
}

public interface IOrganizerEngine
{
    /// <summary>
    /// Organise the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    void Organize(IOrganizerEngineRule rule, IContentBase[] entities);

    /// <summary>
    /// Cleanup the folders linked to the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    void Cleanup(IOrganizerEngineRule rule, IContentBase[] entities);
}