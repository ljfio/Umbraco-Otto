// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core;
using Our.Umbraco.Otto.Core.Selectors;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Otto.Notifications;

public class ContentMovingToRecycleBinHandler : INotificationHandler<ContentMovingToRecycleBinNotification>
{
    private readonly IOrganizerSelector<IContent> _selector;

    public ContentMovingToRecycleBinHandler(IOrganizerSelector<IContent> selector) => _selector = selector;

    public void Handle(ContentMovingToRecycleBinNotification notification) =>
        _selector.Organize(notification.MoveInfoCollection.Select(e => e.Entity).ToArray(), OrganizerMode.Cleanup);
}