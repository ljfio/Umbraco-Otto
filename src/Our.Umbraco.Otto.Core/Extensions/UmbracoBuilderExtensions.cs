// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core.Organizers;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Otto.Core.Extensions;

public static class UmbracoBuilderExtensions
{
    public static OrganizerCollectionBuilder<IContent> ContentOrganizerStrategies(this IUmbracoBuilder builder) =>
        builder.WithCollectionBuilder<OrganizerCollectionBuilder<IContent>>();
    
    public static OrganizerCollectionBuilder<IMedia> MediaOrganizerStrategies(this IUmbracoBuilder builder) =>
        builder.WithCollectionBuilder<OrganizerCollectionBuilder<IMedia>>();
}