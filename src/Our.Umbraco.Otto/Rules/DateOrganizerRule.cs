// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Otto.Core;
using Our.Umbraco.Otto.Core.Extensions;
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

    public IReadOnlyCollection<string> ParentTypes { get; set; } = [];

    public IReadOnlyCollection<string> ItemTypes { get; set; } = [];

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
        if (IsItemType(entity) && IsParentOrFolder(parent))
            return MatchType.Entity;

        if (IsParent(entity))
            return MatchType.Parent;

        if (IsFolder(entity))
            return MatchType.Folder;

        return MatchType.None;
    }

    private bool IsItemType(IContentBase entity) =>
        !ItemTypes.Any() || entity.HasContentType(ItemTypes);

    private bool IsParentOrFolder(IContentBase entity) =>
        IsParent(entity) || IsFolder(entity);

    private bool IsParent(IContentBase entity) =>
        entity.HasContentType(ParentTypes);

    private bool IsFolder(IContentBase entity) =>
        entity.HasContentType(Day.FolderType) ||
        entity.HasContentType(Month.FolderType) ||
        entity.HasContentType(Year.FolderType);
}