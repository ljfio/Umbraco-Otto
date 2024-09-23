using Our.Umbraco.Organizers.Core.Services;
using Our.Umbraco.Organizers.Core.Strategies;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Strategies;

[OrganizerStrategy("Taxonomy")]
public class TaxonomyMediaOrganizerStrategy(IOrganizerService<IMedia> organizerService) 
    : TaxonomyOrganizerStrategy<IMedia>(organizerService);