// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.Entities;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.AutoFolders.Core.FolderEngine;

public abstract class FolderEngineBase
{
    private readonly IContentService _contentService;
    private readonly IMediaService _mediaService;
    private readonly IEntityService _entityService;

    protected FolderEngineBase(
        IContentService contentService,
        IMediaService mediaService,
        IEntityService entityService)
    {
        _contentService = contentService;
        _mediaService = mediaService;
        _entityService = entityService;
    }

    /// <summary>
    /// Saves the item using the correct service
    /// </summary>
    /// <param name="entity"></param>
    protected void Save(IEntity entity)
    {
        if (entity is IContent content)
        {
            if (content.Published)
                _contentService.SaveAndPublish(content);
            else
                _contentService.Save(content);
        }

        if (entity is IMedia media)
            _mediaService.Save(media);
    }

    /// <summary>
    /// Deletes the item using the correct service
    /// </summary>
    /// <param name="entity"></param>
    protected void Delete(IEntity entity)
    {
        if (entity is IContent content)
            _contentService.Delete(content);

        if (entity is IMedia media)
            _mediaService.Delete(media);
    }

    /// <summary>
    /// Creates a folder under the parent item
    /// </summary>
    /// <param name="folderType"></param>
    /// <param name="parentId"></param>
    /// <param name="name"></param>
    /// <param name="isMedia"></param>
    /// <returns></returns>
    protected IContentBase CreateFolder(string folderType, int parentId, string name, bool isMedia)
    {
        if (isMedia)
            return _mediaService.CreateMedia(name, parentId, folderType);

        return _contentService.Create(name, parentId, folderType);
    }

    /// <summary>
    /// Gets all of the folders under the parent item
    /// </summary>
    /// <param name="folderType"></param>
    /// <param name="parentId"></param>
    /// <returns></returns>
    protected IEntitySlim[] GetFolders(string folderType, int parentId) =>
        _entityService.GetChildren(parentId)
            .OfType<IContentEntitySlim>()
            .Where(e => e.ContentTypeAlias.Equals(folderType))
            .ToArray<IEntitySlim>();

    /// <summary>
    /// Checks if the item has children
    /// </summary>
    /// <param name="parentId"></param>
    /// <param name="isMedia"></param>
    /// <returns></returns>
    protected bool HasChildren(int parentId, bool isMedia) => isMedia switch
    {
        true => _mediaService.HasChildren(parentId),
        false => _contentService.HasChildren(parentId),
    };
}