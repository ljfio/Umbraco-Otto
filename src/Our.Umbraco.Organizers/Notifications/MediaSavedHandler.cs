// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Organizers.Notifications;

public class MediaSavedHandler : INotificationHandler<MediaSavedNotification>
{
    private readonly IOrganizer<IMedia> _organizer;

    public MediaSavedHandler(IOrganizer<IMedia> organizer) => _organizer = organizer;

    public void Handle(MediaSavedNotification notification) => 
        _organizer.Organize(notification.SavedEntities, OrganizerMode.Organize);
}