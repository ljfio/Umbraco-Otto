// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Our.Umbraco.Otto.Tests.Integration.Core;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using UmbracoTestWeb.Models;
using ModelsTag = UmbracoTestWeb.Models.Tag;

namespace Our.Umbraco.Otto.Tests.Integration.Organizers;

[Collection("Web")]
public class TaxonomyOrganizerTests : IDisposable
{
    private readonly IServiceProvider _services;

    public TaxonomyOrganizerTests(TestWebApplicationFactory factory)
    {
        // var configuredFactory = factory.WithWebHostBuilder(config =>
        // {
        //     config.ConfigureServices(services =>
        //         services.Configure<OrganizerSettings>(settings =>
        //         {
        //             settings.Content = new()
        //             {
        //                 Rules =
        //                 [
        //                     new TaxonomyOrganizerRule
        //                     {
        //                         FolderType = BlogCategory.ModelTypeAlias,
        //                         PropertyAlias = "tag",
        //                         ParentTypes =
        //                         [
        //                             Blog.ModelTypeAlias,
        //                         ],
        //                         ItemTypes =
        //                         [
        //                             Article.ModelTypeAlias,
        //                         ]
        //                     }
        //                 ]
        //             };
        //         }));
        // });

        _services = factory.Services;
    }

    [Fact]
    public void ShouldOrganizeEntityInFolder()
    {
        var contentService = _services.GetRequiredService<IContentService>();

        var (tag, blog) = CreateBaseSite(contentService);

        var articleContent = contentService.Create("Article", blog.Id, Article.ModelTypeAlias);
        articleContent.SetValue("tag", tag.GetUdi());

        contentService.SaveAndPublish(articleContent)
            .Should()
            .Match<PublishResult>(v => v.Success);

        var category = contentService.GetParent(articleContent.Id);

        category.Should()
            .Match<IContent>(v => v.ContentType.Alias == BlogCategory.ModelTypeAlias)
            .And
            .Match<IContent>(v => v.ParentId == blog.Id)
            .And
            .Match<IContent>(v => string.Equals(v.Name, tag.Name, StringComparison.CurrentCulture));
    }

    private (IContent Tag, IContent Blog) CreateBaseSite(IContentService contentService)
    {
        var homeContent = contentService.Create("Home", Constants.System.Root, Home.ModelTypeAlias);
        contentService.SaveAndPublish(homeContent)
            .Should()
            .Match<PublishResult>(v => v.Success);

        var sharedContent = contentService.Create("Shared", Constants.System.Root, Shared.ModelTypeAlias);
        contentService.SaveAndPublish(sharedContent)
            .Should()
            .Match<PublishResult>(v => v.Success);

        var tagGroupContent = contentService.Create("Tags", sharedContent.Id, TagGroup.ModelTypeAlias);
        contentService.SaveAndPublish(tagGroupContent)
            .Should()
            .Match<PublishResult>(v => v.Success);

        var exampleTagContent = contentService.Create("Example", sharedContent.Id, ModelsTag.ModelTypeAlias);
        contentService.SaveAndPublish(exampleTagContent)
            .Should()
            .Match<PublishResult>(v => v.Success);

        var blogContent = contentService.Create("Blog", homeContent.Id, Blog.ModelTypeAlias);
        contentService.SaveAndPublish(blogContent)
            .Should()
            .Match<PublishResult>(v => v.Success);

        return (exampleTagContent, blogContent);
    }

    public void Dispose()
    {
        var contentService = _services.GetRequiredService<IContentService>();

        foreach (var content in contentService.GetRootContent())
            contentService.Delete(content);
    }
}