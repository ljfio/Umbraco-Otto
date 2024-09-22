// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.AutoFolders.Config;
using Our.Umbraco.AutoFolders.Core.FolderEngine;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Entities;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.AutoFolders.FolderEngine;

[FolderEngine("Taxonomy")]
public class TaxonomyFolderEngine : FolderEngineBase, IFolderEngine<TaxonomyFolderEngineRule>
{
    public TaxonomyFolderEngine(
        IContentService contentService,
        IMediaService mediaService,
        IEntityService entityService) :
        base(
            contentService,
            mediaService,
            entityService)
    {
    }

    public void Organise(TaxonomyFolderEngineRule rule, IContentBase[] entities)
    {
        foreach (var entity in entities)
        {
            // Get all folders
            var folders = GetFolders(rule.FolderType, entity.ParentId, entity is IMedia);

            // Get the tag from the entity using the property alias
            var tag = entity.HasProperty(rule.PropertyAlias) ? entity.GetValue(rule.PropertyAlias)?.ToString() : null;

            if (string.IsNullOrEmpty(tag))
                continue;

            // Find a matching folder to put the entity into
            var matching = folders.FirstOrDefault(folder => tag.Equals(folder.Name));

            if (matching is not null)
            {
                entity.SetParent(matching);
            }
            else
            {
                // Create the folder if it does not exist
                var folder = CreateFolder(rule.FolderType, entity.ParentId, tag, entity is IMedia);

                Save(folder, entity is IContent { Published: true });

                entity.SetParent(folder);
            }

            Save(entity);
        }
    }

    public void Cleanup(TaxonomyFolderEngineRule rule, IContentBase[] entities)
    {
        foreach (var entity in entities)
        {
            // Get all folders
            var folders = GetFolders(rule.FolderType, entity.ParentId, entity is IMedia);

            var tag = entity.HasProperty(rule.PropertyAlias) ? entity.GetValue(rule.PropertyAlias)?.ToString() : null;

            if (string.IsNullOrEmpty(tag))
                continue;

            var matching = folders.FirstOrDefault(folder => tag.Equals(folder.Name));

            // If the folder exists and there is no children, delete the folder
            if (matching is not null && !HasChildren(matching.Id, matching is IMedia))
                Delete(matching);
        }
    }
}