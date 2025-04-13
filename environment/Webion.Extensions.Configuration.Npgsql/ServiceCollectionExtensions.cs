using Microsoft.Extensions.Configuration;

namespace Webion.Extensions.Configuration.Npgsql;

public static class ServiceCollectionExtensions
{
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