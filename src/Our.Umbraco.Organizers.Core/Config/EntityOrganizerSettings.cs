namespace Our.Umbraco.Organizers.Core.Config;

public class EntityOrganizerSettings
{
    public SortOrder DefaultSortOrder { get; set; }

    public IEnumerable<IOrganizerEngineRule> Rules { get; set; } = [];
}