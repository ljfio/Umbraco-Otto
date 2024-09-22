// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Organizers.Notifications;

public class MediaDeletedHandler : INotificationHandler<MediaDeletedNotification>
{
    private readonly IOrganizer _organizer;

    public MediaDeletedHandler(IOrganizer organizer) => _organizer = organizer;

    public void Handle(MediaDeletedNotification notification) => _organizer.Cleanup(notification.DeletedEntities);
}