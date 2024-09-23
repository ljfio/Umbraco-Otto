using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core.Config;

public abstract class OrganizerEngineRuleBase : IOrganizerEngineRule
{
    public string Engine { get; set; } = string.Empty;

    public string PropertyAlias { get; set; } = string.Empty;

    public SortOrder SortOrder { get; set; }

    public IEnumerable<string> ParentTypes { get; set; } = [];

    public IEnumerable<string> ItemTypes { get; set; } = [];

    public string FolderType { get; set; } = string.Empty;

    public virtual bool Matches(IContentBase entity, IContentBase parent, OrganizerMode mode) => mode switch
    {
        OrganizerMode.Organize => IsParentOrFolder(parent) && IsMatchingType(entity),
        OrganizerMode.Cleanup => IsParentOrFolder(parent) && IsMatchingType(entity),
    };

    private bool IsParentOrFolder(IContentBase parent) =>
        ParentTypes.Contains(parent.ContentType.Alias) ||
        FolderType.Equals(parent.ContentType.Alias);

    private bool IsMatchingType(IContentBase entity) => 
        !ItemTypes.Any() || 
        ItemTypes.Contains(entity.ContentType.Alias);
}