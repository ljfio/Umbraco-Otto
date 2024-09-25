// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.Extensions.Options;
using Our.Umbraco.Otto.Core.Config;
using Our.Umbraco.Otto.Core.Organizers;
using Our.Umbraco.Otto.Core.Rules;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Otto.Selectors;

public class MediaOrganizerSelector : OrganizerSelectorBase<IMedia>
{
    private readonly IMediaService _mediaService;
    private readonly IOptions<OrganizerSettings> _options;

    public MediaOrganizerSelector(
        OrganizerCollection<IMedia> collection,
        IMediaService mediaService,
        IOptions<OrganizerSettings> options) :
        base(collection)
    {
        _mediaService = mediaService;
        _options = options;
    }

    protected override IEnumerable<IOrganizerRule> GetRules() => _options.Value.Media.Rules;

    protected override IMedia? GetParent(IMedia entity) => _mediaService.GetParent(entity);
}