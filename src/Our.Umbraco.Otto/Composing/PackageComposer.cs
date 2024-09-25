// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Our.Umbraco.Otto.Core.Config;
using Our.Umbraco.Otto.Core.Extensions;
using Our.Umbraco.Otto.Core;
using Our.Umbraco.Otto.Core.Services;
using Our.Umbraco.Otto.Config;
using Our.Umbraco.Otto.Core.Selectors;
using Our.Umbraco.Otto.Notifications;
using Our.Umbraco.Otto.Organizers;
using Our.Umbraco.Otto.Selectors;
using Our.Umbraco.Otto.Services;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Notifications;

namespace Our.Umbraco.Otto.Composing;

public class PackageComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.Configure<OrganizerSettings>(builder.Config.GetSection(PackageConstants.PackageName));

        builder.Services.AddSingleton<IConfigureOptions<JsonOptions>, ConfigureJsonOptions>();

        builder.Services
            .AddSingleton<IOrganizerService<IContent>, ContentOrganizerService>()
            .AddSingleton<IOrganizerService<IMedia>, MediaOrganizerService>()
            .AddSingleton<IOrganizerSelector<IMedia>, MediaOrganizerSelector>()
            .AddSingleton<IOrganizerSelector<IContent>, ContentOrganizerSelector>();
        
        builder
            .AddNotificationHandler<ContentSavedNotification, ContentSavedHandler>()
            .AddNotificationHandler<ContentDeletedNotification, ContentDeletedHandler>()
            .AddNotificationHandler<ContentMovedToRecycleBinNotification, ContentMovedToRecycleBinHandler>()
            .AddNotificationHandler<MediaSavedNotification, MediaSavedHandler>()
            .AddNotificationHandler<MediaDeletedNotification, MediaDeletedHandler>()
            .AddNotificationHandler<MediaMovedToRecycleBinNotification, MediaMovedToRecycleBinHandler>();

        builder.MediaOrganizers()
            .Add<TaxonomyMediaOrganizer>();
        
        builder.ContentOrganizers()
            .Add<TaxonomyContentOrganizer>();
    }
}