using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core.Rules;

public interface IOrganizerRule
{
    string Strategy { get; }
    
    bool Matches(IContentBase entity, IContentBase parent, OrganizerMode mode);
}