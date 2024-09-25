// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using System.Reflection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Our.Umbraco.Otto.Core.Organizers;

public class OrganizerCollection<TEntity> : BuilderCollectionBase<Type>
    where TEntity : class, IContentBase
{
    public OrganizerCollection(Func<IEnumerable<Type>> items) : base(items)
    {
    }

    public Type? FindOrganizerByName(string name) => this
        .FirstOrDefault(type => type.HasCustomAttribute<OrganizerAttribute>(false) &&
                                type.GetCustomAttribute<OrganizerAttribute>()?.Name == name);
}