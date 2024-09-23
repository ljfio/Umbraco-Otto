// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

namespace Our.Umbraco.Organizers.Core.Strategies;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class OrganizerStrategyAttribute : Attribute
{
    public OrganizerStrategyAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}