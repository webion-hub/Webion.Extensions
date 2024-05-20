using Microsoft.Extensions.DependencyInjection;

namespace Webion.Extensions.DependencyInjection;

public static class ModuleExtension
{
    public static void AddModulesFromAssemblyContaining<T>(this IServiceCollection services)
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

    public static void AddModule<T>(this IServiceCollection services)
        where T: IModule, new()
    {
        new T().Configure(services);
    }
}