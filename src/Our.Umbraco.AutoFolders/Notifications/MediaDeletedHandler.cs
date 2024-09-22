// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.AutoFolders.FolderEngine;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.AutoFolders.Notifications;

public class MediaDeletedHandler : INotificationHandler<MediaDeletedNotification>
{
    private readonly IFolderEngineDispatcher _dispatcher;

    public MediaDeletedHandler(IFolderEngineDispatcher dispatcher) => _dispatcher = dispatcher;

    public void Handle(MediaDeletedNotification notification) => _dispatcher.Cleanup(notification.DeletedEntities);
}