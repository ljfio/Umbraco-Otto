// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.Extensions.DependencyInjection;
using Our.Umbraco.AutoFolders.Notifications;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.AutoFolders.Composing;

public class PackageComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder
            .AddNotificationHandler<ContentSavedNotification, ContentSavedHandler>()
            .AddNotificationHandler<ContentDeletedNotification, ContentDeletedHandler>()
            .AddNotificationHandler<ContentMovedToRecycleBinNotification, ContentMovedToRecycleBinHandler>()
            .AddNotificationHandler<MediaSavedNotification, MediaSavedHandler>()
            .AddNotificationHandler<MediaDeletedNotification, MediaDeletedHandler>()
            .AddNotificationHandler<MediaMovedToRecycleBinNotification, MediaMovedToRecycleBinHandler>();
    }
}