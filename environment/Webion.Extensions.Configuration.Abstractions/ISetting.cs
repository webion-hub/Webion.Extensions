namespace Webion.Extensions.Configuration.Abstractions;

/// <summary>
/// Represents a configuration setting section within an application.
/// </summary>
public interface ISetting
{
    public string Section { get; }
}