using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Webion.Extensions.Configuration.Npgsql;

internal sealed class NpgsqlConfigurationProvider : ConfigurationProvider
{
    private readonly NpgsqlConfigurationSource _source;

    public NpgsqlConfigurationProvider(NpgsqlConfigurationSource source)
    {
        _source = source;
    }
    

    public override void Load()
    {
        using var conn = new NpgsqlConnection(_source.ConnectionString);
        var query =
            $"""
             select key, value
             from {_source.SchemaName}.{_source.TableName}
             where @environment = ANY(environments)
             """;
        
        var settings = conn.Query<AppSettingDbo>(query, new
        {
            _source.Environment
        });
        
        foreach (var setting in settings)
        {
            Data[setting.Key] = setting.Value;
        }
    }
}