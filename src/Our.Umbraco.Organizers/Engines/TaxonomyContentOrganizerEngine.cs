// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Engines;
using Our.Umbraco.Organizers.Core.Services;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Engines;

[OrganizerEngine("Taxonomy")]
public class TaxonomyContentOrganizerEngine(IOrganizerService<IContent> organizerService) 
    : TaxonomyOrganizerEngine<IContent>(organizerService);