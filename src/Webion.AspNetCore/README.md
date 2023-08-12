Basic AspNetCore extensions

Configuration classes
---

Configuration classes can be used to group together related services or application configurations.

```csharp
public sealed class SwaggerConfig : IWebApplicationConfiguration
{
    public void Add(WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            var definitionScheme = new OpenApiSecurityScheme
            {
                // ...
            };

            var requirementScheme = new OpenApiSecurityScheme
            {
                // ...
            };

            options.AddSecurityDefinition("Bearer", definitionScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                [requirementScheme] = Array.Empty<string>(),
            });
        });
    }

    public void Use(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
}
```

```csharp
// Program.cs

var builder = WebApplication.CreateBuilder(args);

builder.Add<SwaggerConfig>();

var app = builder.Build();

app.Use<SwaggerConfig>();

app.Run();
```