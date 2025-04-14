using Dapper;
using Npgsql;

namespace Webion.Extensions.Configuration.Npgsql;

public sealed class NpgsqlConfigurationSeeder
{
    /// <summary>
    /// Seeds application settings into the specified PostgreSQL database by determining missing
    /// or updated configuration entries and inserting or updating them in the application's settings table.
    /// </summary>
    /// <param name="settings">A collection of dictionaries containing key-value pairs representing the application settings to be processed.</param>
    /// <param name="connectionString">The connection string used to connect to the PostgreSQL database.</param>
    /// <param name="environment">The application environment identifier used to segment settings data.</param>
    /// <param name="schemaName">The name of the schema containing the settings table. Defaults to "public".</param>
    /// <param name="tableName">The name of the table where settings are stored. Defaults to "app_setting".</param>
    /// <returns>An asynchronous task representing the operation of seeding or updating the specified settings in the database.</returns>
    public static async Task SeedAppSettingsAsync(
        IEnumerable<IDictionary<string, string?>> settings,
        string connectionString,
        string environment,
        string schemaName = "public",
        string tableName = "app_setting"
    )
    {
        await using var db = new NpgsqlConnection(connectionString);
        var query =
            $"""
             select key 
             from {schemaName}.{tableName}
             where environment = @environment
             """;

        var keys = await db.QueryAsync<string>(query, new
        {
            environment = environment
        });
        
        var all = settings
            .SelectMany(x => x)
            .Where(x => !keys.Contains(x.Key))
            .ToDictionary(x => x.Key, x => x.Value);
        
        var inserts = "";
        foreach (var (k, v) in all)
        {
            inserts += 
                $"""
                 insert into {schemaName}.{tableName} (key, value, environment) 
                 values ('{k}', '{v}', '{environment}') 
                 on conflict (key, environment) 
                     do update set value = '{v}';
                 """;
        }
    
        if (string.IsNullOrWhiteSpace(inserts))
            return;
        
        await db.ExecuteAsync(inserts);
    }
}