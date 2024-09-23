using Our.Umbraco.Organizers.Core.Rules;

namespace Our.Umbraco.Organizers.Core.Config;

public class EntityOrganizerSettings
{
    public SortOrder DefaultSortOrder { get; set; }

    public IEnumerable<IOrganizerRule> Rules { get; set; } = [];
}