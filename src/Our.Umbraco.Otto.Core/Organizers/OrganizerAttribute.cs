// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

namespace Our.Umbraco.Otto.Core.Organizers;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class OrganizerAttribute : Attribute
{
    public OrganizerAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}