// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Engines;
using Umbraco.Cms.Core.DependencyInjection;

namespace Our.Umbraco.Organizers.Core.Extensions;

public static class UmbracoBuilderExtensions
{
    public static OrganizerEngineCollectionBuilder OrganizerEngines(this IUmbracoBuilder builder) =>
        builder.WithCollectionBuilder<OrganizerEngineCollectionBuilder>();
}