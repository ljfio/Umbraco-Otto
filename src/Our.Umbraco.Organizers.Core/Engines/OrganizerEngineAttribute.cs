// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

namespace Our.Umbraco.Organizers.Core.Engines;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class OrganizerEngineAttribute : Attribute
{
    public OrganizerEngineAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}