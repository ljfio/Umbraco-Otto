// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core;
using Our.Umbraco.Otto.Core.Selectors;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Otto.Notifications;

public class ContentDeletedHandler : INotificationHandler<ContentDeletedNotification>
{
    private readonly IOrganizerSelector<IContent> _organizerSelector;

    public ContentDeletedHandler(IOrganizerSelector<IContent> organizerSelector) => _organizerSelector = organizerSelector;

    public void Handle(ContentDeletedNotification notification) => 
        _organizerSelector.Organize(notification.DeletedEntities, OrganizerMode.Cleanup);
}