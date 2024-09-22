// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Organizers.Notifications;

public class ContentSavedHandler : INotificationHandler<ContentSavedNotification>
{
    private readonly IOrganizer _dispatcher;

    public ContentSavedHandler(IOrganizer dispatcher) => _dispatcher = dispatcher;

    public void Handle(ContentSavedNotification notification) => _dispatcher.Organise(notification.SavedEntities);
}