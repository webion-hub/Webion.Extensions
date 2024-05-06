namespace Webion.AspNetCore;

public interface IWebApplicationConfiguration
{
    void Add(WebApplicationBuilder builder);
    void Use(WebApplication app);
}