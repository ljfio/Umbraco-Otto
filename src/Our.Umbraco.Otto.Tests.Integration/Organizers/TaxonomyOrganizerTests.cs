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
    private readonly IContentService _contentService;

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
        _contentService = _services.GetRequiredService<IContentService>();
    }

    [Fact]
    public void ShouldOrganizeEntityInFolder()
    {
        var site = CreateBaseSite();

        var article = _contentService.Create("Article", site.Blog.Id, Article.ModelTypeAlias);
        article.SetValue("tag", site.Tag.GetUdi());

        _contentService.SaveAndPublish(article)
            .Should()
            .Match<PublishResult>(v => v.Success);

        // Check that the parent of this article is a blog category with expected name
        var category = _contentService.GetParent(article.Id);

        category.Should()
            .Match<IContent>(v => v.ContentType.Alias == BlogCategory.ModelTypeAlias, "it is a blog category")
            .And
            .Match<IContent>(v => v.ParentId == site.Blog.Id, "parent is the blog")
            .And
            .Match<IContent>(v => string.Equals(v.Name, site.Tag.Name, StringComparison.CurrentCulture), "it has the same name as the tag");
    }

    [Fact]
    public void ShouldCleanupOldCategories()
    {
        var site = CreateBaseSite();

        var category = CreateAndPublish("Childless", site.Blog.Id, BlogCategory.ModelTypeAlias);

        var article = _contentService.Create("Article", site.Blog.Id, Article.ModelTypeAlias);
        article.SetValue("tag", site.Tag.GetUdi());

        _contentService.SaveAndPublish(article)
            .Should()
            .Match<PublishResult>(v => v.Success);
        
        int parentId = article.ParentId;
        
        _contentService.Delete(article)
            .Should()
            .Match<OperationResult>(v => v.Success);

        _contentService.GetById(parentId)
            .Should()
            .BeNull();

        _contentService.GetById(category.Id)
            .Should()
            .BeNull();
    }

    private BaseSite CreateBaseSite()
    {
        var home = CreateAndPublish("Home", Constants.System.Root, Home.ModelTypeAlias);
        var blog = CreateAndPublish("Blog", home.Id, Blog.ModelTypeAlias);
        
        var shared = CreateAndPublish("Shared", Constants.System.Root, Shared.ModelTypeAlias);
        var tagGroup = CreateAndPublish("Tags", shared.Id, TagGroup.ModelTypeAlias);
        var tag = CreateAndPublish("Tag", shared.Id, ModelsTag.ModelTypeAlias);

        return new BaseSite(home, blog, shared, tagGroup, tag);
    }

    private IContent CreateAndPublish(string name, int parentId, string documentTypeAlias)
    {
        var content = _contentService.Create(name, parentId, documentTypeAlias);

        _contentService.SaveAndPublish(content)
            .Should()
            .Match<PublishResult>(v => v.Success);

        return content;
    }

    public void Dispose()
    {
        var contentService = _services.GetRequiredService<IContentService>();

        foreach (var content in contentService.GetRootContent())
            contentService.Delete(content);
    }

    private record BaseSite(IContent Home, IContent Blog, IContent Shared, IContent TagGroup, IContent Tag);
}