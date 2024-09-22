// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.AutoFolders.Notifications;

public class MediaMovedToRecycleBinHandler : INotificationHandler<MediaMovedToRecycleBinNotification>
{
    public void Handle(MediaMovedToRecycleBinNotification notification)
    {
        throw new NotImplementedException();
    }
}