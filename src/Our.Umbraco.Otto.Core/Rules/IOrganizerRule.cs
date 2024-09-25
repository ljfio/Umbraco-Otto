using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Otto.Core.Rules;

public interface IOrganizerRule
{
    string Organizer { get; }
    
    MatchType Matches(IContentBase entity, IContentBase parent, OrganizerMode mode);
}