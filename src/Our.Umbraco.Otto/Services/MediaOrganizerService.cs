// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core.Services;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Otto.Services;

public class MediaOrganizerService : IOrganizerService<IMedia>
{
    private readonly IMediaService _mediaService;

    public MediaOrganizerService(IMediaService mediaService) => _mediaService = mediaService;

    /// <inheritdoc />
    public void Save(IMedia entity) => _mediaService.Save(entity);

    /// <inheritdoc />
    public void Delete(IMedia entity) => _mediaService.Delete(entity);

    /// <inheritdoc />
    public IMedia CreateFolder(string name, int parentId, string folderType) =>
        _mediaService.CreateMedia(name, parentId, folderType);

    /// <inheritdoc />
    public IEnumerable<IMedia> GetFolders(int id, string folderType) => 
        _mediaService.GetPagedChildren(id, 0, 100, out _);

    /// <inheritdoc />
    public IMedia? GetParent(IMedia entity) => _mediaService.GetParent(entity);

    /// <inheritdoc />
    public bool HasChildren(int id) => _mediaService.HasChildren(id);
}