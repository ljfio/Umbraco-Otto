using J2N.Numerics;
using Our.Umbraco.Otto.Core.Services;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Otto.Services;

public class ContentOrganizerService : IOrganizerService<IContent>
{
    private readonly IContentService _contentService;
    private readonly IEntityService _entityService;
    private readonly IRelationService _relationService;

    public ContentOrganizerService(
        IContentService contentService,
        IEntityService entityService,
        IRelationService relationService)
    {
        _contentService = contentService;
        _entityService = entityService;
        _relationService = relationService;
    }

    /// <inheritdoc />
    public void Save(IContent entity, bool publish = false)
    {
        if (entity.Published || publish)
            _contentService.SaveAndPublish(entity);
        else
            _contentService.Save(entity);
    }

    /// <inheritdoc />
    public void Move(IContent entity, int parentId) => _contentService.Move(entity, parentId);

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
    public string? GetValue(IContent entity, string propertyAlias)
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
    public bool HasChildren(int id) =>
        _contentService.HasChildren(id);

    /// <inheritdoc />
    public IContent? GetOriginalParent(IContent entity)
    {
        if (!entity.Trashed)
            return _contentService.GetById(entity.ParentId);

        var relation = _relationService
            .GetByChild(entity, Constants.Conventions.RelationTypes.RelateParentDocumentOnDeleteAlias)
            .FirstOrDefault();

        return relation is null ? null : _contentService.GetById(relation.ParentId);
    }
}