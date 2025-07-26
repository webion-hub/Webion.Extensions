using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Webion.Db.Migrations.Abstractions;

namespace Webion.Db.Migrations;

internal sealed class MigrationConfigurationBuilder : IMigrationConfigurationBuilder
{
    public MigrationConfigurationBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; }


    public IMigrationConfigurationBuilder AddMigrationsFromAssemblyContaining<T>()
    {
        return AddMigrationsFromAssembly(typeof(T).Assembly);
    }

    public IMigrationConfigurationBuilder AddMigrationsFromAssembly(Assembly assembly)
    {
        var migrations = assembly
            .GetTypes()
            .Where(t => t is { IsAbstract: false, IsInterface: false })
            .Where(t => typeof(IMigration).IsAssignableFrom(t))
            .Select(t => ServiceDescriptor.Transient(typeof(IMigration), t));

        Services.TryAddEnumerable(migrations);
        return this;
    }
}