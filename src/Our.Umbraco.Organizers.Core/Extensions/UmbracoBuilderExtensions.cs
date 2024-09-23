// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Strategies;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core.Extensions;

public static class UmbracoBuilderExtensions
{
    public static OrganizerStrategyCollectionBuilder<IContent> ContentOrganizerStrategies(this IUmbracoBuilder builder) =>
        builder.WithCollectionBuilder<OrganizerStrategyCollectionBuilder<IContent>>();
    
    public static OrganizerStrategyCollectionBuilder<IMedia> MediaOrganizerStrategies(this IUmbracoBuilder builder) =>
        builder.WithCollectionBuilder<OrganizerStrategyCollectionBuilder<IMedia>>();
}