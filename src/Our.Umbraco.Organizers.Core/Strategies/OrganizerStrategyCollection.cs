// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using System.Reflection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Our.Umbraco.Organizers.Core.Strategies;

public class OrganizerStrategyCollection<TEntity> : BuilderCollectionBase<Type>
    where TEntity : class, IContentBase
{
    public OrganizerStrategyCollection(Func<IEnumerable<Type>> items) : base(items)
    {
    }

    public Type? FindStrategyType(string name) => this
        .FirstOrDefault(type => type.HasCustomAttribute<OrganizerStrategyAttribute>(false) &&
                                type.GetCustomAttribute<OrganizerStrategyAttribute>()?.Name == name);
}