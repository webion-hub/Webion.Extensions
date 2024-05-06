namespace Webion.AspNetCore;

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