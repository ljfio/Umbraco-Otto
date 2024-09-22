// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.Extensions.Options;
using Our.Umbraco.Organizers.Core.Config;
using Our.Umbraco.Organizers.Core.Engines;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Organizers;

public class ContentOrganizer : OrganizerBase<IContent>
{
    private readonly IContentService _contentService;
    private readonly IOptions<OrganizerSettings> _options;

    public ContentOrganizer(
        OrganizerEngineCollection<IContent> engineCollection,
        IServiceProvider serviceProvider,
        IContentService contentService,
        IOptions<OrganizerSettings> options) :
        base(
            engineCollection,
            serviceProvider)
    {
        _contentService = contentService;
        _options = options;
    }

    protected override IEnumerable<IOrganizerEngineRule> GetRules() => _options.Value.Content.Rules;

    protected override IContent? GetParent(IContent entity) => _contentService.GetParent(entity);
}