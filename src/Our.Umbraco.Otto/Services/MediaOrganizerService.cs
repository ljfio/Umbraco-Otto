// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core.Services;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Otto.Services;

public class MediaOrganizerService : IOrganizerService<IMedia>
{
    private readonly IMediaService _mediaService;
    private readonly IEntityService _entityService;

    public MediaOrganizerService(
        IMediaService mediaService,
        IEntityService entityService)
    {
        _mediaService = mediaService;
        _entityService = entityService;
    }

    /// <inheritdoc />
    public void Save(IMedia entity, bool publish = false) => _mediaService.Save(entity);

    /// <inheritdoc />
    public void Move(IMedia entity, int parentId) => _mediaService.Move(entity, parentId);

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
    public string? GetValue(IMedia entity, string propertyAlias)
    {
        if (!entity.HasProperty(propertyAlias))
            return null;
        
        var value = entity.GetValue(propertyAlias);
        
        if (value is string stringValue && 
            UdiParser.TryParse(stringValue, out GuidUdi? udi))
            return _entityService.Get(udi.Guid)?.Name;
        
        return value?.ToString();
    }

    /// <inheritdoc />
    public bool HasChildren(int id) => _mediaService.HasChildren(id);
}