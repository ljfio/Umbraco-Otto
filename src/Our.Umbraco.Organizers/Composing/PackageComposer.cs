// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Our.Umbraco.Organizers.Core.Config;
using Our.Umbraco.Organizers.Core.Extensions;
using Our.Umbraco.Organizers.Config;
using Our.Umbraco.Organizers.Core;
using Our.Umbraco.Organizers.Core.Services;
using Our.Umbraco.Organizers.Notifications;
using Our.Umbraco.Organizers.Services;
using Our.Umbraco.Organizers.Strategies;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Organizers.Composing;

public class PackageComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.Configure<OrganizerSettings>(builder.Config.GetSection(PackageConstants.PackageName));

        builder.Services.AddSingleton<IConfigureOptions<JsonOptions>, ConfigureJsonOptions>();

        builder.Services
            .AddSingleton<IOrganizerService<IContent>, ContentOrganizerService>()
            .AddSingleton<IOrganizerService<IMedia>, MediaOrganizerService>()
            .AddSingleton<IOrganizer<IMedia>, MediaOrganizer>()
            .AddSingleton<IOrganizer<IContent>, ContentOrganizer>();
        
        builder
            .AddNotificationHandler<ContentSavedNotification, ContentSavedHandler>()
            .AddNotificationHandler<ContentDeletedNotification, ContentDeletedHandler>()
            .AddNotificationHandler<ContentMovedToRecycleBinNotification, ContentMovedToRecycleBinHandler>()
            .AddNotificationHandler<MediaSavedNotification, MediaSavedHandler>()
            .AddNotificationHandler<MediaDeletedNotification, MediaDeletedHandler>()
            .AddNotificationHandler<MediaMovedToRecycleBinNotification, MediaMovedToRecycleBinHandler>();

        builder.MediaOrganizerStrategies()
            .Add<TaxonomyMediaOrganizerStrategy>();
        
        builder.ContentOrganizerStrategies()
            .Add<TaxonomyContentOrganizerStrategy>();
    }
}