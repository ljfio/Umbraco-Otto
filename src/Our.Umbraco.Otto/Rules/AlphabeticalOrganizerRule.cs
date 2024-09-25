using Our.Umbraco.Otto.Core.Rules;

namespace Our.Umbraco.Otto.Rules;

public class AlphabeticalOrganizerRule : OrganizerRuleBase
{
    public int NumberOfCharacters { get; set; } = 1;
}