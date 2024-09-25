// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Otto.Core.Selectors;

public interface IOrganizerSelector<in TEntity> where TEntity : class, IContentBase
{
    /// <summary>
    /// Invokes the organizer to organize the <paramref name="entities"/> with the appropriate <paramref name="mode"/>
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="mode"></param>
    void Organize(IEnumerable<TEntity> entities, OrganizerMode mode);
}