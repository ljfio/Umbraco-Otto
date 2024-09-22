// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

namespace Our.Umbraco.AutoFolders.Core.Config;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class FolderEngineRuleNameAttribute : Attribute
{
    public FolderEngineRuleNameAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}