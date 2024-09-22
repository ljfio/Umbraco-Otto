// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Composing;

namespace Our.Umbraco.Organizers.Core.Engines;

public class OrganizerEngineCollectionBuilder : TypeCollectionBuilderBase<OrganizerEngineCollectionBuilder, OrganizerEngineCollection, IOrganizerEngine>
{
    protected override OrganizerEngineCollectionBuilder This => this;
}