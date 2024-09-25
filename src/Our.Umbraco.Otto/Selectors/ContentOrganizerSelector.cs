// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.Extensions.Options;
using Our.Umbraco.Otto.Core.Config;
using Our.Umbraco.Otto.Core.Organizers;
using Our.Umbraco.Otto.Core.Rules;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Otto.Selectors;

public class ContentOrganizerSelector : OrganizerSelectorBase<IContent>
{
    private readonly IContentService _contentService;
    private readonly IOptions<OrganizerSettings> _options;

    public ContentOrganizerSelector(
        OrganizerCollection<IContent> collection,
        IContentService contentService,
        IOptions<OrganizerSettings> options) :
        base(collection)
    {
        _contentService = contentService;
        _options = options;
    }

    protected override IEnumerable<IOrganizerRule> GetRules() => _options.Value.Content.Rules;

    protected override IContent? GetParent(IContent entity) => _contentService.GetParent(entity);
}