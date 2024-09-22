// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Config;
using Our.Umbraco.Organizers.Core.Engines;
using Our.Umbraco.Organizers.Core.Services;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Engines;

public abstract class TaxonomyOrganizerEngine<TEntity> : IOrganizerEngine<TaxonomyOrganizerEngineRule, TEntity>
    where TEntity : class, IContentBase
{
    private readonly IOrganizerService<TEntity> _organizerService;

    protected TaxonomyOrganizerEngine(IOrganizerService<TEntity> organizerService)
    {
        _organizerService = organizerService;
    }

    public void Organize(TaxonomyOrganizerEngineRule rule, TEntity[] entities)
    {
        foreach (var entity in entities)
        {
            // Get all folders
            var folders = _organizerService.GetFolders(entity.ParentId, rule.FolderType);

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
                var folder = _organizerService.CreateFolder(tag, entity.ParentId, rule.FolderType);

                _organizerService.Save(folder);

                entity.SetParent(folder);
            }

            _organizerService.Save(entity);
        }
    }

    public void Cleanup(TaxonomyOrganizerEngineRule rule, TEntity[] entities)
    {
        foreach (var entity in entities)
        {
            // Get all folders
            var folders = _organizerService.GetFolders(entity.ParentId, rule.FolderType);

            var tag = entity.HasProperty(rule.PropertyAlias) ? entity.GetValue(rule.PropertyAlias)?.ToString() : null;

            if (string.IsNullOrEmpty(tag))
                continue;

            var matching = folders.FirstOrDefault(folder => tag.Equals(folder.Name));

            // If the folder exists and there is no children, delete the folder
            if (matching is not null && !_organizerService.HasChildren(matching.Id))
                _organizerService.Delete(matching);
        }
    }
}