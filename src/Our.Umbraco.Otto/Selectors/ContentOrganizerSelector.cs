// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.Extensions.Options;
using Our.Umbraco.Otto.Core.Config;
using Our.Umbraco.Otto.Core.Organizers;
using Our.Umbraco.Otto.Core.Rules;
using Our.Umbraco.Otto.Core.Services;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Otto.Selectors;

public class ContentOrganizerSelector : OrganizerSelectorBase<IContent>
{
    private readonly IOrganizerService<IContent> _organizerService;
    private readonly IOptions<OrganizerSettings> _options;

    public ContentOrganizerSelector(
        OrganizerCollection<IContent> collection,
        IOrganizerService<IContent> organizerService,
        IOptions<OrganizerSettings> options) :
        base(collection)
    {
        _organizerService = organizerService;
        _options = options;
    }

    protected override IEnumerable<IOrganizerRule> GetRules() => _options.Value.Content.Rules;

    protected override IContent? GetParent(IContent entity) => entity.Trashed
        ? _organizerService.GetOriginalParent(entity)
        : _organizerService.GetParent(entity);
}