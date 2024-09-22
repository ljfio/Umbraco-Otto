// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.AutoFolders.Core.Config;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.AutoFolders.Core.FolderEngine;

public interface IFolderEngine<in TRule>
    where TRule : IFolderEngineRule
{
    /// <summary>
    /// Organise the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    void Organise(TRule rule, IContentBase[] entities);

    /// <summary>
    /// Cleanup the folders linked to the <paramref name="entities"/> based on the <paramref name="rule"/>
    /// </summary>
    /// <param name="rule"></param>
    /// <param name="entities"></param>
    void Cleanup(TRule rule, IContentBase[] entities);
}

public interface IFolderEngine : IFolderEngine<IFolderEngineRule>;