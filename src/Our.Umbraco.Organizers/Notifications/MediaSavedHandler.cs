// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.FolderEngine;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Organizers.Notifications;

public class MediaSavedHandler : INotificationHandler<MediaSavedNotification>
{
    private readonly IFolderEngineDispatcher _dispatcher;

    public MediaSavedHandler(IFolderEngineDispatcher dispatcher) => _dispatcher = dispatcher;

    public void Handle(MediaSavedNotification notification) => _dispatcher.Organise(notification.SavedEntities);
}