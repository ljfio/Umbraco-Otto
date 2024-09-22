// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Config;
using Our.Umbraco.Organizers.Core.Engines;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Engines;

[OrganizerEngine("Date")]
public class DateOrganizerEngine<TEntity> : IOrganizerEngine<DateOrganizerEngineRule, TEntity>
    where TEntity : class, IContentBase
{
    public void Organize(DateOrganizerEngineRule rule, TEntity[] entities)
    {
        throw new NotImplementedException();
    }

    public void Cleanup(DateOrganizerEngineRule rule, TEntity[] entities)
    {
        throw new NotImplementedException();
    }
}