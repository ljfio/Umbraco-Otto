// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Strategies;
using Our.Umbraco.Organizers.Rules;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Organizers.Strategies;

[OrganizerStrategy("Date")]
public class DateOrganizerStrategy<TEntity> : IOrganizerStrategy<DateOrganizerRule, TEntity>
    where TEntity : class, IContentBase
{
    public OperationResult Organize(DateOrganizerRule rule, TEntity[] entities)
    {
        throw new NotImplementedException();
    }

    public OperationResult Cleanup(DateOrganizerRule rule, TEntity[] entities)
    {
        throw new NotImplementedException();
    }
}