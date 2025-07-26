# Webion.Db.Migrations.Typesense
Typesense-specific implementation of the Webion database migration framework. This library provides concrete implementations for managing schema migrations and data changes in Typesense search engine deployments.

## Key Components
- **`TypesenseMigrationStore`** - Implements `IMigrationStore` for tracking applied migrations in Typesense
- **`TypesenseMigrationsOptions`** - Configuration options for Typesense migration behavior
- **`ServiceCollectionExtensions`** - Provides `AddTypesenseMigrations()` extension method for dependency injection setup

## Features
- Native Typesense integration for migration tracking
- Configurable migration options through `TypesenseMigrationsOptions`
- Seamless integration with .NET Core dependency injection
- Async migration execution with cancellation token support

## Usage
Register Typesense migrations in your service collection:
``` csharp
services.AddTypesenseMigrations(connectionString);
```
## Framework Support
- Built on top of `Webion.Db.Migrations.Abstractions`
- Designed specifically for Typesense search engine
- Supports async/await patterns for optimal performance

This package enables structured schema management and data migrations for Typesense deployments within the Webion migration framework ecosystem.
