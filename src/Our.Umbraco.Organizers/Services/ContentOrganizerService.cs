using Our.Umbraco.Organizers.Core.Services;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Organizers.Services;

public class ContentOrganizerService : IOrganizerService<IContent>
{
    private readonly IContentService _contentService;

    public ContentOrganizerService(IContentService contentService) => _contentService = contentService;

    /// <inheritdoc />
    public void Save(IContent entity)
    {
        if (entity.Published)
            _contentService.SaveAndPublish(entity);
        else
            _contentService.Save(entity);
    }

    /// <inheritdoc />
    public void Delete(IContent entity) =>
        _contentService.Delete(entity);

    /// <inheritdoc />
    public IContent CreateFolder(string name, int parentId, string folderType) =>
        _contentService.Create(name, parentId, folderType);

    /// <inheritdoc />
    public IEnumerable<IContent> GetFolders(int parentId, string folderType) =>
        _contentService.GetPagedChildren(parentId, 0, 100, out _);

    /// <inheritdoc />
    public IContent? GetParent(IContent entity) => _contentService.GetParent(entity);

    /// <inheritdoc />
    public bool HasChildren(int id) =>
        _contentService.HasChildren(id);
}