using Our.Umbraco.Organizers.Core.Engines;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Engines;

[OrganizerEngine("Taxonomy")]
public class TaxonomyMediaOrganizerEngine(IOrganizerService<IMedia> organizerService) 
    : TaxonomyOrganizerEngine<IMedia>(organizerService);