// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using System.Reflection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Our.Umbraco.Organizers.Core.Engines;

public class OrganizerEngineCollection<TEntity> : BuilderCollectionBase<Type>
    where TEntity : class, IContentBase
{
    public OrganizerEngineCollection(Func<IEnumerable<Type>> items) : base(items)
    {
    }

    public Type? FindEngineType(string name) => this
        .FirstOrDefault(type => type.HasCustomAttribute<OrganizerEngineAttribute>(false) &&
                                type.GetCustomAttribute<OrganizerEngineAttribute>()?.Name == name);
}