# Webion.Db.Migrations
A .NET database migration framework that provides a structured approach to managing database schema changes and data migrations. This project is part of the Webion.Extensions ecosystem and works alongside the abstractions layer and specialized implementations (such as Typesense support).

## Features
- Structured database migration management
- Integration with .NET Core dependency injection
- Support for multiple database providers through abstraction layer
- Extensible architecture for custom migration implementations

## Framework Support
- Targets .NET 9.0
- Compatible with .NET Standard 2.1 for broader compatibility

This project works in conjunction with:
- Core interfaces and contracts `Webion.Db.Migrations.Abstractions`
- Typesense-specific migration implementation `Webion.Db.Migrations.Typesense`
