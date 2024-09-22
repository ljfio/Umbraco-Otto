// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Organizers.Notifications;

public class MediaSavedHandler : INotificationHandler<MediaSavedNotification>
{
    private readonly IOrganizer _dispatcher;

    public MediaSavedHandler(IOrganizer dispatcher) => _dispatcher = dispatcher;

    public void Handle(MediaSavedNotification notification) => _dispatcher.Organize(notification.SavedEntities);
}