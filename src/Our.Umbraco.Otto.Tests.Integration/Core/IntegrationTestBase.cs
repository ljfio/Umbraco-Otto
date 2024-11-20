// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;

namespace Our.Umbraco.Otto.Tests.Integration.Core;

public abstract class IntegrationTestBase(TestWebApplicationFactory factory)
{
    protected IServiceProvider Services => factory.Services;

    protected HttpClient HttpClient => factory.CreateClient();

    protected IContentService ContentService => Services.GetRequiredService<IContentService>();

    protected IMediaService MediaService => Services.GetRequiredService<IMediaService>();

    protected IPublishedUrlProvider PublishedUrlProvider => Services.GetRequiredService<IPublishedUrlProvider>();

    private UmbracoContextReference UmbracoContextReference => Services
        .GetRequiredService<IUmbracoContextFactory>()
        .EnsureUmbracoContext();

    protected IUmbracoContext UmbracoContext => UmbracoContextReference.UmbracoContext;
}