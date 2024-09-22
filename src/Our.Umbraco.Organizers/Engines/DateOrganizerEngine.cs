// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Config;
using Our.Umbraco.Organizers.Core.Engines;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Engines;

[OrganizerEngine("Date")]
public class DateOrganizerEngine : IOrganizerEngine<DateOrganizerEngineRule>
{
    public void Organize(DateOrganizerEngineRule rule, IContentBase[] entities)
    {
        throw new NotImplementedException();
    }

    public void Cleanup(DateOrganizerEngineRule rule, IContentBase[] entities)
    {
        throw new NotImplementedException();
    }
}