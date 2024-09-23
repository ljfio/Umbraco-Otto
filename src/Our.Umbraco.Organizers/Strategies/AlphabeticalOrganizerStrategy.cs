// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Rules;
using Our.Umbraco.Organizers.Core.Strategies;
using Our.Umbraco.Organizers.Rules;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Organizers.Strategies;

[OrganizerStrategy("Alphabetical")]
public class AlphabeticalOrganizerStrategy<TEntity> : IOrganizerStrategy<AlphabeticalOrganizerRule, TEntity>
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