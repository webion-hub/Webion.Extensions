using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Webion.Db.Migrations.Abstractions;

namespace Webion.Db.Migrations.Typesense;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Typesense migrations services to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="builder">
    /// The <see cref="IServiceCollection"/> to which the Typesense migration services will be added.
    /// </param>
    /// <param name="collectionName">
    /// The name of the Typesense collection used to store migration metadata. Defaults to "migrations".
    /// </param>
    public static void AddTypesenseMigrations(this IMigrationConfigurationBuilder builder,
        string collectionName = "migrations"
    )
    {
        builder.Services.Configure<TypesenseMigrationsOptions>(o =>
        {
            o.CollectionName = collectionName;
        });

        builder.Services.TryAddTransient<SeedMigration>();
        builder.Services.TryAddSingleton<IMigrationStore, TypesenseMigrationStore>();
    }
}