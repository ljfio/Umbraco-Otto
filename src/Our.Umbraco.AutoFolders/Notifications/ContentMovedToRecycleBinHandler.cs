// Copyright 2023 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.AutoFolders.Notifications;

public class ContentMovedToRecycleBinHandler : INotificationHandler<ContentMovedToRecycleBinNotification>
{
    public void Handle(ContentMovedToRecycleBinNotification notification)
    {
        throw new NotImplementedException();
    }
}