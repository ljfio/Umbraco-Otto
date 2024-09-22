// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core.Engines;

public class OrganizerEngineCollectionBuilder<TEntity> : TypeCollectionBuilderBase<OrganizerEngineCollectionBuilder<TEntity>, OrganizerEngineCollection<TEntity>, IOrganizerEngine<TEntity>>
    where TEntity : class, IContentBase
{
    protected override OrganizerEngineCollectionBuilder<TEntity> This => this;
}