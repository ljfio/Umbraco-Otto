// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core;
using Our.Umbraco.Otto.Core.Selectors;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Otto.Notifications;

public class MediaSavedHandler : INotificationHandler<MediaSavedNotification>
{
    private readonly IOrganizerSelector<IMedia> _organizerSelector;

    public MediaSavedHandler(IOrganizerSelector<IMedia> organizerSelector) => _organizerSelector = organizerSelector;

    public void Handle(MediaSavedNotification notification) => 
        _organizerSelector.Organize(notification.SavedEntities, OrganizerMode.Organize);
}