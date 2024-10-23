// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Our.Umbraco.Otto.Core.Config;
using Our.Umbraco.Otto.Core.Rules;
using Our.Umbraco.Otto.Rules;

namespace Our.Umbraco.Otto.Config;

public class ConfigureJsonOptions : IConfigureOptions<JsonOptions>
{
    public void Configure(JsonOptions options)
    {
        options.JsonSerializerOptions.TypeInfoResolver?
            .WithAddedModifier(static info =>
            {
                if (info.Type != typeof(IOrganizerRule))
                    return;

                // TODO Dynamic polymorphic types
                info.PolymorphismOptions = new()
                {
                    TypeDiscriminatorPropertyName = nameof(IOrganizerRule.Organizer),
                    DerivedTypes =
                    {
                        new(typeof(AlphabeticalOrganizerRule), AlphabeticalOrganizerRule.OrganizerName),
                        new(typeof(DateOrganizerRule), DateOrganizerRule.OrganizerName),
                        new(typeof(TaxonomyOrganizerRule), TaxonomyOrganizerRule.OrganizerName),
                    }
                };
            });
    }
}