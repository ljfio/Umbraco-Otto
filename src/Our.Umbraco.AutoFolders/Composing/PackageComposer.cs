// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Our.Umbraco.AutoFolders.Config;
using Our.Umbraco.AutoFolders.Core.Config;
using Our.Umbraco.AutoFolders.Core.Extensions;
using Our.Umbraco.AutoFolders.FolderEngine;
using Our.Umbraco.AutoFolders.Notifications;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.AutoFolders.Composing;

public class PackageComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.Configure<FolderSettings>(builder.Config.GetSection(PackageConstants.PackageName));

        builder.Services.AddSingleton<IConfigureOptions<JsonOptions>, ConfigureJsonOptions>();

        builder.Services.AddSingleton<IFolderEngineDispatcher, FolderEngineDispatcher>();
        
        builder
            .AddNotificationHandler<ContentSavedNotification, ContentSavedHandler>()
            .AddNotificationHandler<ContentDeletedNotification, ContentDeletedHandler>()
            .AddNotificationHandler<ContentMovedToRecycleBinNotification, ContentMovedToRecycleBinHandler>()
            .AddNotificationHandler<MediaSavedNotification, MediaSavedHandler>()
            .AddNotificationHandler<MediaDeletedNotification, MediaDeletedHandler>()
            .AddNotificationHandler<MediaMovedToRecycleBinNotification, MediaMovedToRecycleBinHandler>();

        builder.FolderEngines()
            .Add<AlphabeticalFolderEngine>()
            .Add<DateFolderEngine>()
            .Add<TaxonomyFolderEngine>();
    }
}