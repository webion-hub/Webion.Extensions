# Webion.Extensions.Configuration.Abstractions

`Webion.Extensions.Configuration.Abstractions` is a lightweight library designed to provide abstractions and utilities for working with application configuration in .NET. It enables developers to define, structure, and dynamically describe configuration settings using a unified interface.

## Features

- **`ISetting` Interface**:
    - A simple abstraction for application configuration sections.
    - Ensures all configuration settings are structured uniformly.

- **`OptionsDescriptor` Utility**:
    - Scans assemblies for classes implementing the `ISetting` interface.
    - Generates key-value mappings of configuration properties with optional hierarchical section prefixes.
    - Simplifies working with complex application settings or nested configuration structures.

## Requirements

- **Target Framework**: `.NET 9.0`

## Installation

You can install this library via [NuGet](https://www.nuget.org/packages/Webion.Extensions.Configuration.Abstractions):

```shell script
dotnet add package Webion.Extensions.Configuration.Abstractions --version 1.0.0
```

## Quick Start

### Define a Configuration Section

Implement the `ISetting` interface to define your configuration settings:

```csharp
using Webion.Extensions.Configuration.Abstractions;

public class AppSettings : ISetting
{
    public string Section => "App";

    public string Environment { get; set; } = "Production";
    public int MaxRetries { get; set; } = 5;
}
```

### Generate Descriptions of Settings

Use the `OptionsDescriptor` utility to inspect and describe the settings:

#### Describe All Settings in an Assembly:

```csharp
var descriptions = OptionsDescriptor.DescribeAssemblyContaining<AppSettings>();

foreach (var (key, value) in descriptions)
{
    Console.WriteLine($"{key}: {value}");
}
// Example Output:
// App:Environment: Production
// App:MaxRetries: 5
```

#### Describe a Specific Settings Class:

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

## Package Metadata

- **Version**: `1.0.0`
- **License**: [MIT](https://licenses.nuget.org/MIT)
- **Repository**: [GitHub Repository](https://github.com/webion-hub/Webion.Extensions)

## Contributing

If you wish to contribute, feel free to open an issue or submit a pull request on the [GitHub repository](https://github.com/webion-hub/Webion.Extensions).

---

Developed and maintained by **Webion SRL**.