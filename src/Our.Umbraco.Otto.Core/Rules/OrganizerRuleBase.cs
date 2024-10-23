using Our.Umbraco.Otto.Core.Extensions;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Otto.Core.Rules;

public abstract class OrganizerRuleBase : IOrganizerRule
{
    public abstract string Organizer { get; }

    public string PropertyAlias { get; set; } = string.Empty;

    public SortOrder SortOrder { get; set; }

    public IReadOnlyCollection<string> ParentTypes { get; set; } = [];

    public IReadOnlyCollection<string> ItemTypes { get; set; } = [];

    public string FolderType { get; set; } = string.Empty;

    public virtual MatchType Matches(IContentBase entity, IContentBase parent, OrganizerMode mode)
    {
        if (IsItemType(entity) && IsParentOrFolder(parent))
            return MatchType.Entity;

        if (IsParent(entity))
            return MatchType.Parent;

        if (IsFolder(entity))
            return MatchType.Folder;

        return MatchType.None;
    }

    protected bool IsItemType(IContentBase entity) =>
        !ItemTypes.Any() || entity.HasContentType(ItemTypes);

    protected bool IsParentOrFolder(IContentBase entity) =>
        IsParent(entity) || IsFolder(entity);

    protected bool IsParent(IContentBase entity) =>
        entity.HasContentType(ParentTypes);

    protected bool IsFolder(IContentBase entity) =>
        entity.HasContentType(FolderType);
}