using Our.Umbraco.Organizers.Core.Config;

namespace Our.Umbraco.Organizers.Config;

public class AlphabeticalOrganizerEngineRule : OrganizerEngineRuleBase
{
    public int NumberOfCharacters { get; set; } = 1;
}