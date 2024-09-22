// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Models;

namespace Our.Umbraco.Organizers.Core;

public interface IOrganizer
{
    void Organise(IEnumerable<IContentBase> entities);

    void Cleanup(IEnumerable<IContentBase> entities);
}