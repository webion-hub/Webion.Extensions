using Microsoft.Extensions.Configuration;

namespace Webion.Extensions.Configuration.Npgsql;

internal sealed class NpgsqlConfigurationSource : IConfigurationSource
{
    public string ConnectionString { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public string SchemaName { get; set; } = "public";
    public string TableName { get; set; } = "app_setting";
    
    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new NpgsqlConfigurationProvider(this);
    }
}