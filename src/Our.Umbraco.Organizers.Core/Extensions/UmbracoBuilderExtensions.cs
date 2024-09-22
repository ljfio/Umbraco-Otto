// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Engines;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core.Extensions;

public static class UmbracoBuilderExtensions
{
    public static OrganizerEngineCollectionBuilder<IContent> ContentOrganizerEngines(this IUmbracoBuilder builder) =>
        builder.WithCollectionBuilder<OrganizerEngineCollectionBuilder<IContent>>();
    
    public static OrganizerEngineCollectionBuilder<IMedia> MediaOrganizerEngines(this IUmbracoBuilder builder) =>
        builder.WithCollectionBuilder<OrganizerEngineCollectionBuilder<IMedia>>();
}