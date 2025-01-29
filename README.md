# HPHA.UiPath.Core

HPHA.UiPath.Core is a .NET library for various utilities and functionalities used in UiPath automation projects.

## Features

- **Converters**: Convert data between different formats.
  - DataTable to Entity Converter ([`DataTableToEntityConverter`](src/HPHA.UiPath.Core/Converters/DataTableToEntityConverter.cs))
  - JSON to Entity Converter ([`JsonToEntityConverter`](src/HPHA.UiPath.Core/Converters/JsonToEntityConverter.cs))
- **Formatters**: Format data for specific use cases.
  - Timer Formatter ([`TimerFormatter`](src/HPHA.UiPath.Core/Formatters/TimerFormatter.cs))
- **Image Processing**: Utilities for processing images.
  - Image Region Snipper ([`ImageRegionSnipper`](src/HPHA.UiPath.Core/ImageProcessing/ImageRegionSnipper.cs))
- **Azure Document Intelligence**: Integration with Azure's Document Intelligence services.
  - Tax ([`Tax`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/Tax.cs))
  - Vendor Name ([`VendorName`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/VendorName.cs))
  - Remittance Address ([`RemittanceAddress`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/RemittanceAddress.cs))
  - Customer Name ([`CustomerName`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/CustomerName.cs))
  - Service Start Date ([`ServiceStartDate`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/ServiceStartDate.cs))
  - Product Code ([`ProductCode`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/ProductCode.cs))
  - Invoice ID ([`InvoiceId`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/InvoiceId.cs))
  - Fields ([`Fields`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/Fields.cs))
  - Document ([`Document`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/Document.cs))
  - Analyze Result ([`AnalyzeResult`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/AnalyzeResult.cs))
  - Page ([`Page`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/Page.cs))
  - Table ([`Table`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/Table.cs))
  - Line ([`Line`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/Line.cs))
  - Word ([`Word`](src/HPHA.UiPath.Core/Azure/DocumentIntelligence/Word.cs))

## Installation

To install HPHA.UiPath.Core, add the following package reference to your project file:

```xml
<PackageReference Include="HPHA.UiPath.Core" Version="1.0.0" />
```

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
var entity = converter.Convert(jsonString);
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

## Building the Project

To build the project, run the following command:

```bash
dotnet build
```

## Running Tests

To run the unit tests, use the following command:

```bash
dotnet test
```

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for details.

## Authors

- Vinicius Teruel

## Repository

[HPHA.UiPath.Core Repository](https://dev.azure.com/hpha/Invoice%20Automation/_git/core)
