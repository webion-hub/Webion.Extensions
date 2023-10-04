using Microsoft.Extensions.DependencyInjection;

namespace Webion.Application.Extensions;


public interface IModule
{
    void Configure(IServiceCollection services);
}

public static class ModuleExtension
{
    public static void AddModulesFromAssembly<T>(this IServiceCollection services)
    {
        var assembly = typeof(T).Assembly;
        var moduleTypes = assembly
            .GetTypes()
            .Where(t => t is { IsPublic: true, IsAbstract: false, IsClass: true })
            .Where(t => t.GetInterface(nameof(IModule)) == typeof(IModule));

        foreach (var type in moduleTypes)
        {
            if (Activator.CreateInstance(type) is not IModule module)
                throw new InvalidOperationException("Module must implement IModule interface");

            module.Configure(services);
        }
    }
}