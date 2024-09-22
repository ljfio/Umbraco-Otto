// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Organizers.Notifications;

public class ContentMovedToRecycleBinHandler : INotificationHandler<ContentMovedToRecycleBinNotification>
{
    private readonly IOrganizer<IContent> _organizer;

    public ContentMovedToRecycleBinHandler(IOrganizer<IContent> organizer) => _organizer = organizer;

    public void Handle(ContentMovedToRecycleBinNotification notification) =>
        _organizer.Cleanup(notification.MoveInfoCollection.Select(e => e.Entity).ToArray());
}