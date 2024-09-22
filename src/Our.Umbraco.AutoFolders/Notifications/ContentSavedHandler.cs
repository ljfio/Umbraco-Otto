// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.AutoFolders.Notifications;

public class ContentSavedHandler : INotificationHandler<ContentSavedNotification>
{
    public void Handle(ContentSavedNotification notification)
    {
        throw new NotImplementedException();
    }
}