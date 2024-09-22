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
    /// <param name="publish"></param>
    protected void Save(IEntity entity, bool publish = false)
    {
        if (entity is IContent content)
        {
            if (content.Published || publish)
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
    /// <param name="isMedia"></param>
    /// <returns></returns>
    protected IEnumerable<IContentBase> GetFolders(string folderType, int parentId, bool isMedia)
    {
        if (isMedia)
            return _mediaService.GetPagedChildren(parentId, 0, 100, out _);

        return _contentService.GetPagedChildren(parentId, 0, 100, out _);
    }

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