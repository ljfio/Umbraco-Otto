namespace Our.Umbraco.Organizers.Core.Config;

public class FolderSettings
{
    public SortOrder DefaultSortOrder { get; set; }

    public EntityFolderSettings Content { get; set; } = new();

    public EntityFolderSettings Media { get; set; } = new();
}