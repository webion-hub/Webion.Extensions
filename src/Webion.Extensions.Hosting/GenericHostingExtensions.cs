using Microsoft.Extensions.Hosting;

namespace Webion.Extensions.Hosting;

public static class GenericHostingExtensions
{
    public static void Add<TConfig, TBuilder, THost>(this TBuilder builder)
        where TBuilder: IHostBuilder
        where THost: IHost
        where TConfig: IHostConfiguration<TBuilder, THost>, new()
    {
        new TConfig().Add(builder);
    }
    
    public static void Use<TConfig, TBuilder, THost>(this THost app)
        where TBuilder: IHostBuilder
        where THost: IHost
        where TConfig: IHostConfiguration<TBuilder, THost>, new()
    {
        new TConfig().Use(app);
    }
}