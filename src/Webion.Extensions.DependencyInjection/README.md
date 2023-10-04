DependencyInjection extensions

Modules
---

Modules can be used to add related services together.

It is generally used with services that are declared and/or injected within the same (vertical) slice. 

```csharp
public sealed class JwtModule : IModule
{
    public void Configure(IServiceCollection services)
    {
        services.AddTransient<IJwtIssuer, JwtIssuer>();
    }
}

public sealed class SearchModule : IModule
{
    public void Configure(IServiceCollection services)
    {
        services.AddTransient<IBookingLocator, BookingLocator>();
        services.AddTransient<IPriceLocator, PriceLocator>();
    }
}
```

```csharp
// Program.cs
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddModulesFromAssembly<Program>();
```