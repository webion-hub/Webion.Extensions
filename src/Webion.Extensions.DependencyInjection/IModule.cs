using Microsoft.Extensions.DependencyInjection;

namespace Webion.Extensions.DependencyInjection;


public interface IModule
{
    void Configure(IServiceCollection services);
}