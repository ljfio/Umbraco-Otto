// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Organizers.Notifications;

public class MediaMovedToRecycleBinHandler : INotificationHandler<MediaMovedToRecycleBinNotification>
{
    private readonly IOrganizer<IMedia> _organizer;

    public MediaMovedToRecycleBinHandler(IOrganizer<IMedia> organizer) => _organizer = organizer;

    public void Handle(MediaMovedToRecycleBinNotification notification) =>
        _organizer.Cleanup(notification.MoveInfoCollection.Select(e => e.Entity).ToArray());
}