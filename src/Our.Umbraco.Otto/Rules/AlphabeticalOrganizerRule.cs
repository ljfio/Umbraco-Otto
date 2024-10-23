using Our.Umbraco.Otto.Core.Rules;

namespace Our.Umbraco.Otto.Rules;

public class AlphabeticalOrganizerRule : OrganizerRuleBase
{
    public const string OrganizerName = "Alphabetical";
    
    public override string Organizer => OrganizerName;
    
    public int NumberOfCharacters { get; set; } = 1;
}