// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Our.Umbraco.Organizers.Core.Config;

namespace Our.Umbraco.Organizers.Config;

public class ConfigureJsonOptions : IConfigureOptions<JsonOptions>
{
    public void Configure(JsonOptions options)
    {
        options.JsonSerializerOptions.TypeInfoResolver?
            .WithAddedModifier(static info =>
            {
                if (info.Type != typeof(IOrganizerEngineRule))
                    return;

                // TODO Dynamic polymorphic types
                info.PolymorphismOptions = new()
                {
                    TypeDiscriminatorPropertyName = "Engine",
                    DerivedTypes =
                    {
                        new(typeof(AlphabeticalOrganizerEngineRule), "Alphabetical"),
                        new(typeof(DateOrganizerEngineRule), "Date"),
                        new(typeof(TaxonomyOrganizerEngineRule), "Taxonomy"),
                    }
                };
            });
    }
}