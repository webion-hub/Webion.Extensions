# Webion.Extensions.Configuration.Abstractions

`Webion.Extensions.Configuration.Abstractions` is a lightweight library designed to provide utilities for working with application configuration in .NET. It simplifies describing and managing configuration settings dynamically through a structured approach.

## Features

- **`OptionsDescriptor` Utility**:
  - Automatically scans configuration types and generates descriptions of their properties.
  - Supports nested configurations with hierarchical structure.
  - Enables easy debugging and visualization of configuration values.

## Requirements

- **Target Framework**: `.NET 9.0`

## Installation

You can install the library via [NuGet](https://www.nuget.org/packages/Webion.Extensions.Configuration.Abstractions):

```textmate
dotnet add package Webion.Extensions.Configuration.Abstractions --version 1.0.1
```

## Quick Start

### Generate Descriptions of Configuration Classes

`OptionsDescriptor` provides methods to describe the configuration properties of a class type dynamically.

#### Describe All Properties of a Specific Class

To describe all properties of a specific configuration class:

```csharp
var appDetails = OptionsDescriptor.Describe<AppSettings>();

foreach (var (key, value) in appDetails)
{
    Console.WriteLine($"{key}: {value}");
}
// Example Output:
// App:Environment: Production
// App:MaxRetries: 5
```

### Support for Nested Properties

`OptionsDescriptor` automatically handles nested properties, generating keys in a hierarchical manner. For example, a nested property structure will output keys as `Parent:Child:Property`.

## Package Metadata

- **Version**: `1.0.1`
- **License**: [MIT](https://licenses.nuget.org/MIT)
- **Repository**: [GitHub Repository](https://github.com/webion-hub/Webion.Extensions)

## Contributing

If you wish to contribute, feel free to open an issue or submit a pull request on the [GitHub repository](https://github.com/webion-hub/Webion.Extensions).

---

Developed and maintained by **Webion SRL**.