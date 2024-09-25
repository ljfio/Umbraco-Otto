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

public class MediaOrganizerSelector : OrganizerSelectorBase<IMedia>
{
    private readonly IOrganizerService<IMedia> _organizerService;
    private readonly IOptions<OrganizerSettings> _options;

    public MediaOrganizerSelector(
        OrganizerCollection<IMedia> collection,
        IOrganizerService<IMedia> organizerService,
        IOptions<OrganizerSettings> options) :
        base(collection)
    {
        _organizerService = organizerService;
        _options = options;
    }

    protected override IEnumerable<IOrganizerRule> GetRules() => _options.Value.Media.Rules;

    protected override IMedia? GetParent(IMedia entity) => entity.Trashed
        ? _organizerService.GetOriginalParent(entity)
        : _organizerService.GetParent(entity);
}