namespace Our.Umbraco.AutoFolders.Core.Config;

public class FolderSettings
{
    public SortOrder DefaultSortOrder { get; set; }

    public EntityFolderSettings Content { get; set; } = new();

    public EntityFolderSettings Media { get; set; } = new();
}