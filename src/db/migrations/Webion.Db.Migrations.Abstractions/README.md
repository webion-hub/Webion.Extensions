# Webion.Db.Migrations.Abstractions
Core abstractions and interfaces for the Webion database migration framework. This library defines the fundamental contracts and interfaces that enable a pluggable, provider-agnostic approach to database migrations.

## Key Interfaces
- **`IMigration`** - Defines the contract for individual migration implementations
- **`IMigrationStore`** - Provides abstraction for tracking and retrieving applied migrations with methods like `GetAppliedMigrationsAsync()`

## Purpose
This abstractions layer allows for:
- Provider-agnostic migration definitions
- Consistent migration tracking across different database systems
- Extensible architecture for custom migration implementations
- Clean separation between migration logic and storage mechanisms

## Framework Support
- Targets .NET Standard 2.1 for maximum compatibility
- Designed for async/await patterns with `CancellationToken` support

This package serves as the foundation for concrete implementations like and the main framework. `Webion.Db.Migrations.Typesense``Webion.Db.Migrations`
