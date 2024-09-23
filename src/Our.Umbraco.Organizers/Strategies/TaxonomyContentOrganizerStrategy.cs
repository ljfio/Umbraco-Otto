// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Services;
using Our.Umbraco.Organizers.Core.Strategies;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Strategies;

[OrganizerStrategy("Taxonomy")]
public class TaxonomyContentOrganizerStrategy(IOrganizerService<IContent> organizerService) 
    : TaxonomyOrganizerStrategy<IContent>(organizerService);