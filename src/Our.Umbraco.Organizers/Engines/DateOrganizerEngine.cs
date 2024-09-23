// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Config;
using Our.Umbraco.Organizers.Core.Engines;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Organizers.Engines;

[OrganizerEngine("Date")]
public class DateOrganizerEngine<TEntity> : IOrganizerEngine<DateOrganizerEngineRule, TEntity>
    where TEntity : class, IContentBase
{
    public OperationResult Organize(DateOrganizerEngineRule rule, TEntity[] entities)
    {
        throw new NotImplementedException();
    }

    public OperationResult Cleanup(DateOrganizerEngineRule rule, TEntity[] entities)
    {
        throw new NotImplementedException();
    }
}