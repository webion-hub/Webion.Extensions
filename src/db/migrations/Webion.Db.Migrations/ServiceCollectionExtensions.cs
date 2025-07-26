using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Webion.Db.Migrations.Abstractions;

namespace Webion.Db.Migrations;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the <see cref="MigrationRunner"/> service as a hosted service to the service collection.
    /// This enables automatic application of database migrations during application startup.
    /// </summary>
    /// <param name="services">
    /// The <see cref="IServiceCollection"/> to which the <see cref="MigrationRunner"/> service will be added.
    /// </param>
    public static IMigrationConfigurationBuilder AddMigrationRunner(this IServiceCollection services, 
        string? targetMigrationId = null
    )
    {
        services.Configure<MigrationOptions>(o =>
        {
            o.TargetMigrationId = targetMigrationId;
        });
        
        services.TryAddTransient<MigrationApplier>();
        services.AddHostedService<MigrationRunner>();
        return new MigrationConfigurationBuilder(services);
    }
}