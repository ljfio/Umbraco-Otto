// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Config;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core.FolderEngine;

public interface IFolderEngine<in TRule> : IFolderEngine
    where TRule : class, IFolderEngineRule
{
    /// <summary>
    /// Organise the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    void Organise(TRule rule, IContentBase[] entities);

    /// <inheritdoc />
    void IFolderEngine.Organise(IFolderEngineRule rule, IContentBase[] entities)
    {
        if (rule is not TRule typedRule)
            throw new ArgumentException($"must be of type {typeof(TRule).Name}", nameof(rule));
        
        Organise(typedRule, entities);
    }

    /// <summary>
    /// Cleanup the folders linked to the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    void Cleanup(TRule rule, IContentBase[] entities);

    /// <inheritdoc />
    void IFolderEngine.Cleanup(IFolderEngineRule rule, IContentBase[] entities)
    {
        if (rule is not TRule typedRule)
            throw new ArgumentException($"must be of type {typeof(TRule).Name}", nameof(rule));
        
        Cleanup(typedRule, entities);
    }
}

public interface IFolderEngine
{
    /// <summary>
    /// Organise the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    void Organise(IFolderEngineRule rule, IContentBase[] entities);

    /// <summary>
    /// Cleanup the folders linked to the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    void Cleanup(IFolderEngineRule rule, IContentBase[] entities);
}