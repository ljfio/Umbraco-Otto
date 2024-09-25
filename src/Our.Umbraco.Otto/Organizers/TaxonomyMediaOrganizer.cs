using Our.Umbraco.Otto.Core.Organizers;
using Our.Umbraco.Otto.Core.Services;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Otto.Organizers;

[Organizer("Taxonomy")]
public class TaxonomyMediaOrganizer(IOrganizerService<IMedia> organizerService) 
    : TaxonomyOrganizer<IMedia>(organizerService);