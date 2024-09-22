// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.FolderEngine;
using Our.Umbraco.Organizers.Config;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Entities;

namespace Our.Umbraco.Organizers.FolderEngine;

[FolderEngine("Date")]
public class DateFolderEngine : IFolderEngine<DateFolderEngineRule>
{
    public void Organise(DateFolderEngineRule rule, IContentBase[] entities)
    {
        throw new NotImplementedException();
    }

    public void Cleanup(DateFolderEngineRule rule, IContentBase[] entities)
    {
        throw new NotImplementedException();
    }
}