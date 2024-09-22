namespace Our.Umbraco.AutoFolders.Core.Config;

public interface IFolderEngineRule
{
    string PropertyAlias { get; }
    
    SortOrder SortOrder { get; }
    
    IEnumerable<string> ParentTypes { get; }
    
    IEnumerable<string> ItemTypes { get; }
    
    string FolderType { get; }
}