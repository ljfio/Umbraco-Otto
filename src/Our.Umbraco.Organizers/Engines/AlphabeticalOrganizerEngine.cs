// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Config;
using Our.Umbraco.Organizers.Core.FolderEngine;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Engines;

[OrganizerEngine("Alphabetical")]
public class AlphabeticalOrganizerEngine : IOrganizerEngine<AlphabeticalFolderEngineRule>
{
    public void Organize(AlphabeticalFolderEngineRule rule, IContentBase[] entities)
    {
        throw new NotImplementedException();
    }

    public void Cleanup(AlphabeticalFolderEngineRule rule, IContentBase[] entities)
    {
        throw new NotImplementedException();
    }
}