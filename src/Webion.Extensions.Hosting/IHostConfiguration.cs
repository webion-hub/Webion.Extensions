using Microsoft.Extensions.Hosting;

namespace Webion.Extensions.Hosting;

public interface IHostConfiguration<in TBuilder, in THost>
    where TBuilder: IHostBuilder
    where THost: IHost
{
    void Add(TBuilder builder);
    void Use(THost app);
}