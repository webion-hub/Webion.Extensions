# Usage
```csharp
builder.Configuration.AddNpgsql(
    connectionString: $"Host={host};Port={port};User={user};Password={password}",
    environment: builder.Environment.EnvironmentName,
    schemaName: "public",
    tableName: "app_setting"
)
```