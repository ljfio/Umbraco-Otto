namespace Our.Umbraco.Organizers.Core.Config;

public class EntityFolderSettings
{
    public SortOrder DefaultSortOrder { get; set; }

    public IEnumerable<IFolderEngineRule> Rules { get; set; } = [];
}