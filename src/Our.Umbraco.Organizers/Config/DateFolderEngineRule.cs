// Copyright 2024 Luke Fisher
// SPDX-License-Identifier: Apache-2.0

using Our.Umbraco.Organizers.Core.Config;

namespace Our.Umbraco.Organizers.Config;

public class DateFolderEngineRule : FolderEngineRuleBase
{
    public string DayFormat { get; set; } = "dd";
    
    public string MonthFormat { get; set; } = "MM";
    
    public string YearFormat { get; set; } = "YYYY";

    public string DayFolderType { get; set; } = string.Empty;
    
    public string MonthFolderType { get; set; } = string.Empty;
    
    public string YearFolderType { get; set; } = string.Empty;

    public bool CreateDayFolder { get; set; } = true;
    
    public bool CreateMonthFolder { get; set; } = true;
    
    public bool CreateYearFolder { get; set; } = true;
}