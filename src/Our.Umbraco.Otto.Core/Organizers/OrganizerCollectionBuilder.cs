// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Otto.Core.Organizers;

public class OrganizerCollectionBuilder<TEntity> : 
    TypeCollectionBuilderBase<
        OrganizerCollectionBuilder<TEntity>, 
        OrganizerCollection<TEntity>,
        IOrganizer<TEntity>>
    where TEntity : class, IContentBase
{
    protected override OrganizerCollectionBuilder<TEntity> This => this;
}