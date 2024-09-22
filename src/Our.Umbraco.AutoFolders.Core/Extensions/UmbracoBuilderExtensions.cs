// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.AutoFolders.Core.FolderEngine;
using Umbraco.Cms.Core.DependencyInjection;

namespace Our.Umbraco.AutoFolders.Core.Extensions;

public static class UmbracoBuilderExtensions
{
    public static FolderEngineCollectionBuilder FolderEngines(this IUmbracoBuilder builder) =>
        builder.WithCollectionBuilder<FolderEngineCollectionBuilder>();
}