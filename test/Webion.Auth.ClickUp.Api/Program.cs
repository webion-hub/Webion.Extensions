using Microsoft.AspNetCore.Authentication.Cookies;
using Webion.Extensions.AspNetCore.Authentication.ClickUp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Configuration.AddJsonFile("");

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie()
    .AddClickUp(x =>
    {
        x.ClientId = builder.Configuration["ClickUp:ClientId"]!;
        x.ClientSecret = builder.Configuration["ClickUp:ClientSecret"]!;
    });

builder.Services
    .AddAuthorizationBuilder()
    .AddDefaultPolicy("default", x => x
        .AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
    );

var app = builder.Build();

app.Urls.Add("http://localhost:8080");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();