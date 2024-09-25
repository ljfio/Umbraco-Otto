// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core;
using Our.Umbraco.Otto.Core.Organizers;
using Our.Umbraco.Otto.Core.Services;
using Our.Umbraco.Otto.Rules;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using MatchType = Our.Umbraco.Otto.Core.MatchType;

namespace Our.Umbraco.Otto.Organizers;

public abstract class TaxonomyOrganizer<TEntity> : IOrganizer<TaxonomyOrganizerRule, TEntity>
    where TEntity : class, IContentBase
{
    private readonly IOrganizerService<TEntity> _organizerService;

    protected TaxonomyOrganizer(IOrganizerService<TEntity> organizerService)
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
                var tag = _organizerService.GetValue(entity, rule.PropertyAlias);

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

                    _organizerService.Save(folder, entity is IContent { Published: true });

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

            // Get the root folder
            var root = _organizerService.GetRoot(entity, rule.ParentTypes);

            if (root is null)
                continue;

            // Get all organising folders
            var folders = _organizerService.GetFolders(root.Id, rule.FolderType);

            foreach (var folder in folders)
            {
                // If there is no children, delete the folder
                if (!_organizerService.HasChildren(folder.Id))
                    _organizerService.Delete(folder);
            }
        }

        return OperationResult.Succeed(messages);
    }
}