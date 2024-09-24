// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Rules;
using Our.Umbraco.Organizers.Core.Services;
using Our.Umbraco.Organizers.Core.Strategies;
using Our.Umbraco.Organizers.Rules;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using MatchType = Our.Umbraco.Organizers.Core.Rules.MatchType;

namespace Our.Umbraco.Organizers.Strategies;

public abstract class TaxonomyOrganizerStrategy<TEntity> : IOrganizerStrategy<TaxonomyOrganizerRule, TEntity>
    where TEntity : class, IContentBase
{
    private readonly IOrganizerService<TEntity> _organizerService;

    protected TaxonomyOrganizerStrategy(IOrganizerService<TEntity> organizerService)
    {
        _organizerService = organizerService;
    }

    public OperationResult Organize(TaxonomyOrganizerRule rule, IEnumerable<Match<TEntity>> matches)
    {
        var messages = new EventMessages();

        foreach (var match in matches)
        {
            var entity = match.Entity;

            if (match.MatchType == MatchType.Entity)
            {
                // Get the tag from the entity using the property alias
                var tag = entity.HasProperty(rule.PropertyAlias)
                    ? entity.GetValue(rule.PropertyAlias)?.ToString()
                    : null;

                if (string.IsNullOrEmpty(tag))
                    continue;

                // Check that the entity has the correct parent folder
                var parent = _organizerService.GetParent(entity);

                if (parent is null)
                    continue;
                
                if (rule.FolderType.Contains(parent.ContentType.Alias) &&
                    tag.Equals(parent.Name))
                    continue;
                
                // Locate the root
                var root = _organizerService.GetRoot(entity, rule.ParentTypes);
                
                if (root is null)
                    continue;
                
                // Get all folders
                var folders = _organizerService.GetFolders(root.Id, rule.FolderType);

                // Find a matching folder to put the entity into
                var matching = folders.FirstOrDefault(folder => tag.Equals(folder.Name));

                if (matching is not null)
                {
                    if (matching.Id != entity.ParentId)
                    {
                        entity.SetParent(matching);
                        
                        _organizerService.Save(entity);
                    }
                }
                else
                {
                    // Create the folder if it does not exist
                    var folder = _organizerService.CreateFolder(tag, root.Id, rule.FolderType);

                    _organizerService.Save(folder);

                    entity.SetParent(folder);

                    _organizerService.Save(entity);
                }
            }
        }

        return OperationResult.Succeed(messages);
    }

    public OperationResult Cleanup(TaxonomyOrganizerRule rule, IEnumerable<Match<TEntity>> matches)
    {
        var messages = new EventMessages();

        foreach (var match in matches)
        {
            if (match.MatchType != MatchType.Entity)
                continue;

            var entity = match.Entity;

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

        return OperationResult.Succeed(messages);
    }
}