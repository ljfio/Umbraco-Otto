// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core;
using Our.Umbraco.Otto.Core.Organizers;
using Our.Umbraco.Otto.Rules;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Otto.Organizers;

[Organizer("Alphabetical")]
public class AlphabeticalOrganizer<TEntity> : IOrganizer<AlphabeticalOrganizerRule, TEntity>
    where TEntity : class, IContentBase
{
    public OperationResult Organize(AlphabeticalOrganizerRule rule, IEnumerable<Match<TEntity>> entities)
    {
        throw new NotImplementedException();
    }

    public OperationResult Cleanup(AlphabeticalOrganizerRule rule, IEnumerable<Match<TEntity>> entities)
    {
        throw new NotImplementedException();
    }
}