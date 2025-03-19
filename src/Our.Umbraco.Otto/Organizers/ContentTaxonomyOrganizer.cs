// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core.Organizers;
using Our.Umbraco.Otto.Core.Services;
using Our.Umbraco.Otto.Rules;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Otto.Organizers;

[Organizer(TaxonomyOrganizerRule.OrganizerName)]
public class ContentTaxonomyOrganizer(IOrganizerService<IContent> organizerService) 
    : TaxonomyOrganizer<IContent>(organizerService);