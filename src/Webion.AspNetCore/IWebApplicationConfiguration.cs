namespace Webion.AspNetCore;

public interface IWebApplicationConfiguration
{
    void Add(WebApplicationBuilder builder);
    void Use(WebApplication app);
}

public static class WebAppExtensions
{
    public static void Add<TConfig>(this WebApplicationBuilder builder)
        where TConfig: IWebApplicationConfiguration, new()
    {
        new TConfig().Add(builder);
    }
    
    public static void Use<TConfig>(this WebApplication app)
        where TConfig: IWebApplicationConfiguration, new()
    {
        new TConfig().Use(app);
    }
}