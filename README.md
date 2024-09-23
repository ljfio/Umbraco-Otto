# Organizers for Umbraco

## Installing

```poweshell
dotnet add Our.Umbraco.Organizers
```

## Configuring

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

## Rules

### Alphabetical

```json
{
}
```

### Date

```json
{
}
```

### Taxonomy

```json
{
}

```
## Extending

```csharp
builder.ContentOrganizerStrategies()
    .Add<MyCustomContentStrategy>();

builder.MediaOrganizerStrategies()
    .Add<MyCustomMediaStrategy>();
```