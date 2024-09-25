// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core;
using Our.Umbraco.Otto.Core.Selectors;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Otto.Notifications;

public class ContentSavedHandler : INotificationHandler<ContentSavedNotification>
{
    private readonly IOrganizerSelector<IContent> _organizerSelector;

    public ContentSavedHandler(IOrganizerSelector<IContent> organizerSelector) => _organizerSelector = organizerSelector;

    public void Handle(ContentSavedNotification notification) => 
        _organizerSelector.Organize(notification.SavedEntities, OrganizerMode.Organize);
}