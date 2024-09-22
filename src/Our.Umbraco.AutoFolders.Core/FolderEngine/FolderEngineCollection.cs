// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using System.Reflection;
using Umbraco.Cms.Core.Composing;
using Umbraco.Extensions;

namespace Our.Umbraco.AutoFolders.Core.FolderEngine;

public class FolderEngineCollection : BuilderCollectionBase<Type>
{
    public FolderEngineCollection(Func<IEnumerable<Type>> items) : base(items)
    {
    }

    public Type? FindEngineType(string name) => this
        .FirstOrDefault(type => type.HasCustomAttribute<FolderEngineAttribute>(false) &&
                                type.GetCustomAttribute<FolderEngineAttribute>()?.Name == name);
}