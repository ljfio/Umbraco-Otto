// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.FolderEngine;
using Our.Umbraco.Organizers.Config;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.FolderEngine;

[FolderEngine("Alphabetical")]
public class AlphabeticalFolderEngine : IFolderEngine<AlphabeticalFolderEngineRule>
{
    public void Organise(AlphabeticalFolderEngineRule rule, IContentBase[] entities)
    {
        throw new NotImplementedException();
    }

    public void Cleanup(AlphabeticalFolderEngineRule rule, IContentBase[] entities)
    {
        throw new NotImplementedException();
    }
}