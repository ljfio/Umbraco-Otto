// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core.Organizers;
using Our.Umbraco.Otto.Core.Services;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Otto.Organizers;

[Organizer("Taxonomy")]
public class TaxonomyContentOrganizer(IOrganizerService<IContent> organizerService) 
    : TaxonomyOrganizer<IContent>(organizerService);