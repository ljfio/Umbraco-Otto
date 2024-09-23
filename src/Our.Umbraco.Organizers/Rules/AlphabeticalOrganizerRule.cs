using Our.Umbraco.Organizers.Core.Rules;

namespace Our.Umbraco.Organizers.Rules;

public class AlphabeticalOrganizerRule : OrganizerRuleBase
{
    public int NumberOfCharacters { get; set; } = 1;
}