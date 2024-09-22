// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Config;
using Our.Umbraco.Organizers.Core.Engines;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Engines;

[OrganizerEngine("Alphabetical")]
public class AlphabeticalOrganizerEngine<TEntity> : IOrganizerEngine<AlphabeticalOrganizerEngineRule, TEntity>
    where TEntity : class, IContentBase
{
    public void Organize(AlphabeticalOrganizerEngineRule rule, TEntity[] entities)
    {
        throw new NotImplementedException();
    }

    public void Cleanup(AlphabeticalOrganizerEngineRule rule, TEntity[] entities)
    {
        throw new NotImplementedException();
    }
}