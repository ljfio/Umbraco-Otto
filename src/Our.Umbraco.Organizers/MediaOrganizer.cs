// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Microsoft.Extensions.Options;
using Our.Umbraco.Organizers.Core.Config;
using Our.Umbraco.Organizers.Core.Engines;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;

namespace Our.Umbraco.Organizers;

public class MediaOrganizer : OrganizerBase<IMedia>
{
    private readonly IMediaService _mediaService;
    private readonly IOptions<OrganizerSettings> _options;

    public MediaOrganizer(
        OrganizerEngineCollection engineCollection,
        IServiceProvider serviceProvider,
        IMediaService mediaService,
        IOptions<OrganizerSettings> options) :
        base(
            engineCollection,
            serviceProvider)
    {
        _mediaService = mediaService;
        _options = options;
    }

    protected override IEnumerable<IOrganizerEngineRule> GetRules() => _options.Value.Media.Rules;

    protected override IMedia? GetParent(IMedia entity) => _mediaService.GetParent(entity);
}