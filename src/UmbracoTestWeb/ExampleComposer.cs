// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Config;
using Our.Umbraco.Otto.Core.Config;
using Our.Umbraco.Otto.Rules;
using Umbraco.Cms.Core.Composing;
using UmbracoTestWeb.Models;

namespace UmbracoTestWeb;

public class ExampleComposer : IComposer
{
    public void Compose(IUmbracoBuilder builder)
    {
        builder.Services.Configure<OrganizerSettings>(options =>
        {
            options.Content.Rules =
            [
                new TaxonomyOrganizerRule
                {
                    PropertyAlias = "tag",
                    ParentTypes =
                    [
                        Blog.ModelTypeAlias,
                    ],
                    ItemTypes =
                    [
                        Article.ModelTypeAlias,
                    ],
                    FolderType = BlogCategory.ModelTypeAlias,
                },
                new DateOrganizerRule
                {
                    PropertyAlias = "updateDate",
                    ParentTypes =
                    [
                        Events.ModelTypeAlias,
                    ],
                    ItemTypes = [
                    
                        Event.ModelTypeAlias,
                    ],
                    Year =
                    {
                        FolderType = EventYear.ModelTypeAlias,
                        Format = "YYYY",
                        CreateFolder = true,
                    },
                    Month =
                    {
                        FolderType = EventMonth.ModelTypeAlias,
                        Format = "MM",
                        CreateFolder = true,
                    },
                    Day =
                    {
                        CreateFolder = false,
                    }
                },
            ];
        });
    }
}