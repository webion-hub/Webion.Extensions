using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Webion.Extensions.Configuration.Npgsql;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds configuration settings from an Npgsql database to the <see cref="IConfigurationBuilder"/>.
    /// </summary>
    /// <param name="builder">The configuration builder to which the Npgsql configuration source will be added.</param>
    /// <param name="connectionString">The connection string to the Npgsql database.</param>
    /// <param name="environment">The environment name used to filter configuration data.</param>
    /// <param name="schemaName">The schema name in the database where the configuration table resides. Defaults to "public".</param>
    /// <param name="tableName">The name of the table containing configuration data. Defaults to "app_setting".</param>
    /// <returns>The <see cref="IConfigurationBuilder"/> with the added Npgsql configuration source.</returns>
    public static IConfigurationBuilder AddNpgsql(this IConfigurationBuilder builder,
        string connectionString,
        string environment,
        string schemaName = "public",
        string tableName = "app_setting"
    )
    {
        return builder.Add<NpgsqlConfigurationSource>(s =>
        {
            s.ConnectionString = connectionString;
            s.Environment = environment;
            s.SchemaName = schemaName;
            s.TableName = tableName;
        });
    }
}