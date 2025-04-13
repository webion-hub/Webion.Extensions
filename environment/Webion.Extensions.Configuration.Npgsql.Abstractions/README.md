# Webion.Extensions.Configuration.Npgsql.Abstractions

## Overview
`Webion.Extensions.Configuration.Npgsql.Abstractions` is a .NET library providing abstractions to support Npgsql-based configuration providers. It simplifies defining and working with application settings stored in PostgreSQL databases.

The library is part of the Webion.Extensions suite, designed to streamline configuration management in .NET projects that utilize PostgreSQL.

## Key Features
- **Neutral Abstractions**: Provides a foundational abstraction for storing and managing application settings (`AppSettingBaseDbo`).
- **Targeted for modern .NET**: Built for .NET 9.0 and utilizes modern C# features like nullable and implicit usings.
- **Portability**: Includes package metadata for easy NuGet packaging and integration.
- **Documentation**: Distributed with a detailed README and XML-embedded comments for enhanced usability.
- Designed to be part of a broader PostgreSQL configuration provider system.

---

## Installation
1. Add the library to your project through NuGet:
```shell script
dotnet add package Webion.Extensions.Configuration.Npgsql.Abstractions --version 1.0.4
```

2. Or, include it in your `.csproj` file manually:
```xml
<PackageReference Include="Webion.Extensions.Configuration.Npgsql.Abstractions" Version="1.0.4" />
```

---

## Usage

### AppSettingBaseDbo

At the core of the library is the `AppSettingBaseDbo` class. This abstract class is used as a base for defining application settings stored in PostgreSQL. It provides several core properties for configuration management.

### Example:

```csharp
using Webion.Extensions.Configuration.Npgsql.Abstractions;

/// <summary>
/// Defines a specific application setting.
/// </summary>
public class MyAppSetting : AppSettingBaseDbo
{
    public string? Category { get; set; }
}

// Example usage
var setting = new MyAppSetting
{
    Key = "AppName",
    Value = "My Application",
    Description = "Specifies the name of the application.",
    Environment = "Development",
    Category = "General"
};
```

The properties of `AppSettingBaseDbo` are:
- **Key**: The unique key for the setting.
- **Value**: The value associated with the setting.
- **Description** (optional): Additional details about what the setting represents.
- **Environment**: Indicates the deployment environment where the setting applies (e.g., `Development`, `Production`).

---

## Versioning
The current version of the library is **1.0.4**, and it follows [Semantic Versioning](https://semver.org/). Future updates will include enhancements and possible integrations with other Webion Extensions.

---

## Licensing
This library is distributed under the MIT License. More details are available in the [MIT License](https://licenses.nuget.org/MIT) file.

---

## Repository
The source code is hosted on GitHub:
[https://github.com/webion-hub/Webion.Extensions](https://github.com/webion-hub/Webion.Extensions)

For reporting issues or contributing to the project, visit the repository.

--- 

## Acknowledgments
Created and maintained by **Webion SRL**. This library is part of the company's efforts to simplify configuration management in .NET applications.