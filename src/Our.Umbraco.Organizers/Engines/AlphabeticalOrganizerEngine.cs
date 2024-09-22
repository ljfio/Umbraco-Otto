// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Config;
using Our.Umbraco.Organizers.Core.Engines;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Engines;

[OrganizerEngine("Alphabetical")]
public class AlphabeticalOrganizerEngine : IOrganizerEngine<AlphabeticalOrganizerEngineRule>
{
    public void Organize(AlphabeticalOrganizerEngineRule rule, IContentBase[] entities)
    {
        throw new NotImplementedException();
    }

    public void Cleanup(AlphabeticalOrganizerEngineRule rule, IContentBase[] entities)
    {
        throw new NotImplementedException();
    }
}