# Webion.Extensions.Configuration.Npgsql

The `Webion.Extensions.Configuration.Npgsql` library provides an implementation for integrating PostgreSQL as a configuration provider in .NET applications using the `IConfigurationBuilder` mechanism.

## Features

- Load configuration values directly from a PostgreSQL database.
- Filter configuration values by environment.
- Customize schema and table names for storing configuration data.
- Seamless integration with Microsoft.Extensions.Configuration.

## Installation

To include this library in your project, you can add it via NuGet:

```textmate
dotnet add package Webion.Extensions.Configuration.Npgsql --version 1.1
```

Or update your `.csproj` file:

```xml
<PackageReference Include="Webion.Extensions.Configuration.Npgsql" Version="1.1" />
```

## Usage

Below is an example of how to use the `AddNpgsql` extension method to configure your application to load settings from a PostgreSQL database:

```csharp
var builder = new ConfigurationBuilder();

builder.Configuration.AddNpgsql(
    connectionString: $"Host={host};Port={port};User={user};Password={password}",
    environment: builder.Environment.EnvironmentName,
    schemaName: "public",
    tableName: "app_setting"
);
```

### Configuration Table Structure

The PostgreSQL table used to store configuration data must have the following structure:

| Column        | Type   | Description                        |
|---------------|--------|------------------------------------|
| **key**       | string | The configuration key (e.g., "App:Setting:Key"). |
| **value**     | string | The configuration value associated with the key. |
| **environment** | string | Used to filter configurations based on environment (e.g., "Development", "Production"). |

Make sure the table resides in the schema and table name specified in the `AddNpgsql` configuration.

For example, if the default schema (`public`) and table name (`app_setting`) are used, the SQL to create the table might look like this:

```sql
CREATE TABLE public.app_setting (
    key TEXT NOT NULL,
    value TEXT NOT NULL,
    environment TEXT NOT NULL
);
```

## API Reference

### Extension Method: `AddNpgsql`

#### Method Signature
```csharp
public static IConfigurationBuilder AddNpgsql(
    IConfigurationBuilder builder,
    string connectionString,
    string environment,
    string schemaName = "public",
    string tableName = "app_setting"
);
```

#### Parameters

- **builder**: The `IConfigurationBuilder` to which the Npgsql configuration source will be added.
- **connectionString**: The connection string to the PostgreSQL database.
- **environment**: The environment name used to filter configuration values.
- **schemaName**: (Optional) The schema name in the database where the configuration table resides. Defaults to `"public"`.
- **tableName**: (Optional) The name of the table where configuration values are stored. Defaults to `"app_setting"`.

#### Returns

The updated `IConfigurationBuilder` with the added PostgreSQL configuration provider.

## Project Information

- **Title**: Webion.Extensions.Configuration.Npgsql
- **Authors**: Webion SRL
- **Version**: 1.1
- **Target Framework**: .NET 9.0
- **License**: [MIT License](https://licenses.nuget.org/MIT)
- **Repository URL**: [GitHub - Webion.Extensions](https://github.com/webion-hub/Webion.Extensions)

## Dependencies

- [Dapper](https://www.nuget.org/packages/Dapper) (2.1.66)
- [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration) (9.0.4)
- [Npgsql](https://www.nuget.org/packages/Npgsql) (9.0.3)

---

This library is ideal for scenarios where configuration values need to be dynamic, shared across services, or stored in a centralized relational database like PostgreSQL.