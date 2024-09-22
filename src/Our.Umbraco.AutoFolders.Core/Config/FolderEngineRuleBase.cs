namespace Our.Umbraco.AutoFolders.Core.Config;

public abstract class FolderEngineRuleBase : IFolderEngineRule
{
    public string PropertyAlias { get; set; } = string.Empty;

    public SortOrder SortOrder { get; set; }

    public IEnumerable<string> ParentTypes { get; set; } = [];

    public IEnumerable<string> ItemTypes { get; set; } = [];

    public string FolderType { get; set; } = string.Empty;
}