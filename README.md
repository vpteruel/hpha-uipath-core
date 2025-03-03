# HPHA.UiPath.Core

HPHA.UiPath.Core is a .NET library for various utilities and functionalities used in UiPath automation projects.

## Features

- **Converters**: Convert data between different formats.
  - DataTable to Entity Converter
  - JSON to Entity Converter
- **Formatters**: Format data for specific use cases.
  - Timer Formatter
- **Image Processing**: Utilities for processing images.
  - Image Region Snipper
- **Azure Document Intelligence**: Integration with Azure's Document Intelligence services.
  - Converts the json result into an entity.

## Usage

### Converters

**DataTable to Entity Converter**

```csharp
using HPHA.UiPath.Core.Converters;

// Example usage
var converter = new DataTableToEntityConverter();
var entity = converter.Convert(dataTable);
```

**JSON to Entity Converter**

```csharp
using HPHA.UiPath.Core.Converters;

// Example usage
var converter = new JsonToEntityConverter();
var entity = converter.ConvertJsonToEntity(jsonString);
```

### Formatters

**Timer Formatter**

```csharp
using HPHA.UiPath.Core.Formatters;

// Example usage
var formatter = new TimerFormatter();
var formattedTime = formatter.Format(timeSpan);
```

### Image Processing

**Image Region Snipper**

```csharp
using HPHA.UiPath.Core.ImageProcessing;

// Example usage
var (capturedImage, filePath) = ImageRegionSnipper.SnipRegion("example", "input.png", "outputFolder", 10, 10, 100, 100);
```
