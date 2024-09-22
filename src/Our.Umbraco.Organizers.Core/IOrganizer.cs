// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core;

public interface IOrganizer<TEntity> where TEntity : class, IContentBase
{
    void Organize(IEnumerable<TEntity> entities);

    void Cleanup(IEnumerable<TEntity> entities);
}