namespace Webion.Extensions.Configuration.Npgsql.Abstractions;

/// <summary>
/// Represents a base for application settings data.
/// </summary>
/// <remarks>
/// This abstract class serves as a foundation for defining application settings that are stored or managed within a system.
/// </remarks>
/// <remarks>
/// The derived implementation can extend this model to provide explicit functionality or additional properties specific to a given application context.
/// </remarks>
public abstract class AppSettingBaseDbo
{
    public string Key { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Environment { get; set; } = string.Empty;
}