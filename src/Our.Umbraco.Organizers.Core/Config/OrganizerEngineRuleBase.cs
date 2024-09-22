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

    public virtual bool Matches(IContentBase entity, IContentBase parent) =>
        ParentTypes.Contains(parent.ContentType.Alias) &&
        (!ParentTypes.Any() || ItemTypes.Contains(entity.ContentType.Alias));
}