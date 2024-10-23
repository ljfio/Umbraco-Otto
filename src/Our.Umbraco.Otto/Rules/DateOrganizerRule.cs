// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core;
using Our.Umbraco.Otto.Core.Rules;
using Umbraco.Cms.Core.Models;
using MatchType = Our.Umbraco.Otto.Core.MatchType;

namespace Our.Umbraco.Otto.Rules;

public class DateOrganizerRule : IOrganizerRule
{
    public const string OrganizerName = "Date";

    public string Organizer => OrganizerName;

    public string PropertyAlias { get; set; } = string.Empty;

    public SortOrder DefaultSortOrder { get; set; }

    public IEnumerable<string> ParentTypes { get; set; } = [];

    public IEnumerable<string> ItemTypes { get; set; } = [];
    
    public PeriodOptions Day { get; set; } = new()
    {
        Format = "dd",
    };
    
    public PeriodOptions Month { get; set; } = new()
    {
        Format = "MM",
    };
    
    public PeriodOptions Year { get; set; } = new()
    {
        Format = "YYYY",
    };

    public class PeriodOptions
    {
        public string Format { get; set; } = string.Empty;

        public string FolderType { get; set; } = string.Empty;
        
        public bool CreateFolder { get; set; } = true;

        public SortOrder SortOrder { get; set; }
    }
    
    public MatchType Matches(IContentBase entity, IContentBase parent, OrganizerMode mode)
    {
        throw new NotImplementedException();
    }
}