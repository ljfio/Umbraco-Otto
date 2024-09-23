# Organizers for Umbraco

## Installing

```poweshell
dotnet add Our.Umbraco.Organizers
```

## Configuring

To configure the Organizer package, update the Application Settings in your solution with the appropriate rules to use the appropriate organizational strategy for your use case.

```json
{
  "Organizers": {
    "Content": {
      "Rules": []
    },
    "Media": {
      "Rules": []
    }
  }
}
```

## Rules / Strategies

Included with the package are the following strategies that can be used when building your rules.

### Alphabetical

```json
{
  "Strategy": "Alphabetical",
  "PropertyAlias": "",
  "SortOrder": "Ascending",
  "ParentTypes": [],
  "ItemTypes": [],
  "FolderType": "",
  "NumberOfCharacters": 1
}
```

### Date

```json
{
  "Strategy": "Date",
  "PropertyAlias": "",
  "DefaultSortOrder": "Ascending",
  "ParentTypes": [],
  "ItemTypes": [],
  "Day": {
    "Format": "dd",
    "FolderType": "",
    "CreateFolder": true,
    "SortOrder": "Ascending"
  },
  "Month": {
    "Format": "MM",
    "FolderType": "",
    "CreateFolder": true,
    "SortOrder": "Ascending"
  },
  "Year": {
    "Format": "YYYY",
    "FolderType": "",
    "CreateFolder": true,
    "SortOrder": "Ascending"
  }
}
```

### Taxonomy

```json
{
  "Strategy": "Taxonomy",
  "PropertyAlias": "",
  "SortOrder": "Ascending",
  "ParentTypes": [],
  "ItemTypes": [],
  "FolderType": ""
}
```

## Extending

```csharp
builder.ContentOrganizerStrategies()
    .Add<MyCustomContentStrategy>();

builder.MediaOrganizerStrategies()
    .Add<MyCustomMediaStrategy>();
```