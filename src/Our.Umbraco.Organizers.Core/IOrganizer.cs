// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core;

public interface IOrganizer
{
    void Organize(IEnumerable<IContentBase> entities);

    void Cleanup(IEnumerable<IContentBase> entities);
}