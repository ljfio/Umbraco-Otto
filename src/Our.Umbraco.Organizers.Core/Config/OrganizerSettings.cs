namespace Our.Umbraco.Organizers.Core.Config;

public class OrganizerSettings
{
    public SortOrder DefaultSortOrder { get; set; }

    public EntityOrganizerSettings Content { get; set; } = new();

    public EntityOrganizerSettings Media { get; set; } = new();
}