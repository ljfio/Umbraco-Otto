using Our.Umbraco.AutoFolders.Core.Config;

namespace Our.Umbraco.AutoFolders.Config;

public class AlphabeticalFolderEngineRule : FolderEngineRuleBase
{
    public int NumberOfCharacters { get; set; } = 1;
}