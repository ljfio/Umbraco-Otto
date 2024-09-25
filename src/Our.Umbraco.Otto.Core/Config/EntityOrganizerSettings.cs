using Our.Umbraco.Otto.Core.Rules;

namespace Our.Umbraco.Otto.Core.Config;

public class EntityOrganizerSettings
{
    public SortOrder DefaultSortOrder { get; set; }

    public IEnumerable<IOrganizerRule> Rules { get; set; } = [];
}