// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core.Strategies;

public class OrganizerStrategyCollectionBuilder<TEntity> : 
    TypeCollectionBuilderBase<
        OrganizerStrategyCollectionBuilder<TEntity>, 
        OrganizerStrategyCollection<TEntity>,
        IOrganizerStrategy<TEntity>>
    where TEntity : class, IContentBase
{
    protected override OrganizerStrategyCollectionBuilder<TEntity> This => this;
}