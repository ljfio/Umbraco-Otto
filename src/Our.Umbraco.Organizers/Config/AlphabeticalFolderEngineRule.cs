using Our.Umbraco.Organizers.Core.Config;

namespace Our.Umbraco.Organizers.Config;

public class AlphabeticalFolderEngineRule : FolderEngineRuleBase
{
    public int NumberOfCharacters { get; set; } = 1;
}