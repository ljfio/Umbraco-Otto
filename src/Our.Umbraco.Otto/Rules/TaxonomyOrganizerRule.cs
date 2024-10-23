// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core.Rules;

namespace Our.Umbraco.Otto.Rules;

public class TaxonomyOrganizerRule : OrganizerRuleBase
{
    public const string OrganizerName = "Taxonomy";

    public override string Organizer => OrganizerName;
}