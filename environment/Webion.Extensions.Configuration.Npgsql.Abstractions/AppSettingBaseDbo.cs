namespace Webion.Extensions.Configuration.Npgsql.Abstractions;

public abstract class AppSettingBaseDbo
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Environment { get; set; } = string.Empty;
}