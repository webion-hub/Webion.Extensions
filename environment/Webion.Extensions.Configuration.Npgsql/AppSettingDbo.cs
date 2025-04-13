namespace Webion.Extensions.Configuration.Npgsql;

public abstract class AppSettingDbo
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Environment { get; set; } = string.Empty;
}