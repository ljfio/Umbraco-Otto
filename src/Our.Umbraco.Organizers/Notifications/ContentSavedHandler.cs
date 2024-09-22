// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.FolderEngine;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Organizers.Notifications;

public class ContentSavedHandler : INotificationHandler<ContentSavedNotification>
{
    private readonly IFolderEngineDispatcher _dispatcher;

    public ContentSavedHandler(IFolderEngineDispatcher dispatcher) => _dispatcher = dispatcher;

    public void Handle(ContentSavedNotification notification) => _dispatcher.Organise(notification.SavedEntities);
}