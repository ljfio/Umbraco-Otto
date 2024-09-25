// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core;
using Our.Umbraco.Otto.Core.Organizers;
using Our.Umbraco.Otto.Rules;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Otto.Organizers;

[Organizer("Date")]
public class DateOrganizer<TEntity> : IOrganizer<DateOrganizerRule, TEntity>
    where TEntity : class, IContentBase
{
    public OperationResult Organize(DateOrganizerRule rule, IEnumerable<Match<TEntity>> matches)
    {
        throw new NotImplementedException();
    }

    public OperationResult Cleanup(DateOrganizerRule rule, IEnumerable<Match<TEntity>> matches)
    {
        throw new NotImplementedException();
    }
}