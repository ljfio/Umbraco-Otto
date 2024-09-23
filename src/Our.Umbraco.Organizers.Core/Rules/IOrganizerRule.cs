using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core.Rules;

public interface IOrganizerRule
{
    string Strategy { get; }
    
    string PropertyAlias { get; }
    
    SortOrder SortOrder { get; }
    
    IEnumerable<string> ParentTypes { get; }
    
    IEnumerable<string> ItemTypes { get; }

    string FolderType { get; }

    bool Matches(IContentBase entity, IContentBase parent, OrganizerMode mode);
}