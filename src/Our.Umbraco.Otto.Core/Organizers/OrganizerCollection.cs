// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using System.Reflection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Models;
using Umbraco.Extensions;

namespace Our.Umbraco.Otto.Core.Organizers;

public class OrganizerCollection<TEntity> : BuilderCollectionBase<IOrganizer<TEntity>>
    where TEntity : class, IContentBase
{
    public OrganizerCollection(Func<IEnumerable<IOrganizer<TEntity>>> items) : base(items)
    {
    }

    public IOrganizer<TEntity>? FindOrganizerByName(string name) => this
        .FirstOrDefault(organizer => Matches(organizer, name));

    private static bool Matches(IOrganizer<TEntity> organizer, string name)
    {
        var type = organizer.GetType();
        return type.HasCustomAttribute<OrganizerAttribute>(false) &&
               type.GetCustomAttribute<OrganizerAttribute>()?.Name == name;
    }
}