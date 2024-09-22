// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

namespace Our.Umbraco.Organizers.Core.FolderEngine;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class FolderEngineAttribute : Attribute
{
    public FolderEngineAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}