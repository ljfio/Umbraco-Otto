// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Composing;

namespace Our.Umbraco.AutoFolders.Core.FolderEngine;

public class FolderEngineCollection : BuilderCollectionBase<Type>
{
    public FolderEngineCollection(Func<IEnumerable<Type>> items) : base(items)
    {
    }
}