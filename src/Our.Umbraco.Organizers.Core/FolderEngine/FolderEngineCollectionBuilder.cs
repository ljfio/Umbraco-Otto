// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Umbraco.Cms.Core.Composing;

namespace Our.Umbraco.Organizers.Core.FolderEngine;

public class FolderEngineCollectionBuilder : TypeCollectionBuilderBase<FolderEngineCollectionBuilder, FolderEngineCollection, IFolderEngine>
{
    protected override FolderEngineCollectionBuilder This => this;
}