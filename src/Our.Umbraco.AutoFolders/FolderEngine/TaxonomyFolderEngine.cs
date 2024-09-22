// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.AutoFolders.Config;
using Our.Umbraco.AutoFolders.Core.FolderEngine;
using Umbraco.Cms.Core.Models.Entities;

namespace Our.Umbraco.AutoFolders.FolderEngine;

public class TaxonomyFolderEngine : IFolderEngine<TaxonomyFolderEngineRule>
{
    public void Organise(TaxonomyFolderEngineRule rule, IUmbracoEntity[] entities)
    {
        throw new NotImplementedException();
    }

    public void Cleanup(TaxonomyFolderEngineRule rule, IUmbracoEntity[] entities)
    {
        throw new NotImplementedException();
    }
}