// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Our.Umbraco.AutoFolders.Core.Config;

namespace Our.Umbraco.AutoFolders.Config;

public class ConfigureJsonOptions : IConfigureOptions<JsonOptions>
{
    public void Configure(JsonOptions options)
    {
        options.JsonSerializerOptions.TypeInfoResolver?
            .WithAddedModifier(static info =>
            {
                if (info.Type != typeof(IFolderEngineRule))
                    return;

                // TODO Dynamic polymorphic types
                info.PolymorphismOptions = new()
                {
                    TypeDiscriminatorPropertyName = "Engine",
                    DerivedTypes =
                    {
                        new(typeof(AlphabeticalFolderEngineRule), "Alphabetical"),
                        new(typeof(DateFolderEngineRule), "Date"),
                        new(typeof(TaxonomyFolderEngineRule), "Taxonomy"),
                    }
                };
            });
    }
}