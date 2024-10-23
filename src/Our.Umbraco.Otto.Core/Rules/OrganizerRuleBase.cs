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
        if (IsMatchingType(entity) && IsParentOrFolder(parent))
            return MatchType.Entity;

        if (IsParent(entity))
            return MatchType.Parent;

        if (IsFolder(entity))
            return MatchType.Folder;

        return MatchType.None;
    }

    protected bool IsParentOrFolder(IContentBase entity) =>
        IsParent(entity) || IsFolder(entity);

    protected bool IsParent(IContentBase entity) =>
        ParentTypes.Contains(entity.ContentType.Alias);

    protected bool IsFolder(IContentBase entity) =>
        FolderType.Contains(entity.ContentType.Alias);

    protected bool IsMatchingType(IContentBase entity) =>
        !ItemTypes.Any() ||
        ItemTypes.Contains(entity.ContentType.Alias);
}