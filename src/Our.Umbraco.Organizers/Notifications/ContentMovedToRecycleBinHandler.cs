// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.FolderEngine;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Organizers.Notifications;

public class ContentMovedToRecycleBinHandler : INotificationHandler<ContentMovedToRecycleBinNotification>
{
    private readonly IFolderEngineDispatcher _dispatcher;

    public ContentMovedToRecycleBinHandler(IFolderEngineDispatcher dispatcher) => _dispatcher = dispatcher;

    public void Handle(ContentMovedToRecycleBinNotification notification) =>
        _dispatcher.Cleanup(notification.MoveInfoCollection.Select(e => e.Entity).ToArray());
}