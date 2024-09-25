# Otto - Auto Organizer for Umbraco

Automatically organize your content and media into folder structures in the tree.

## Installing

```poweshell
dotnet add Our.Umbraco.Otto
```

## Configuring

To configure the Organizer package, update the Application Settings in your solution with the appropriate rules to apply the appropriate organizer for your use case.

```json
{
  "Otto": {
    "Content": {
      "Rules": []
    },
    "Media": {
      "Rules": []
    }
  }
}
```

## Organizers

Included with the package are the following organizers that can be used.

### Alphabetical

```json
{
  "Organizer": "Alphabetical",
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
  "Organizer": "Date",
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
  "Organizer": "Taxonomy",
  "PropertyAlias": "",
  "SortOrder": "Ascending",
  "ParentTypes": [],
  "ItemTypes": [],
  "FolderType": ""
}
```

## Extending

```csharp
builder.ContentOrganizer()
    .Add<MyCustomContentOrganizer>();

builder.MediaOrganizer()
    .Add<MyCustomMediaOrganizer>();
```