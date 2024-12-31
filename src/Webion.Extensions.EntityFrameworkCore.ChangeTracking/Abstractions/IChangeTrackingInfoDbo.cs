using System.Text.Json;

namespace Webion.Extensions.EntityFrameworkCore.ChangeTracking.Abstractions;

public interface IChangeTrackingInfoDbo
{
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the target entity for change tracking purposes.
    /// </summary>
    public string? TargetId { get; set; }
    public string? EntityName { get; set; }
    
    public decimal Version { get; set; }
    public DateTimeOffset Timestamp { get; set; }

    
    public JsonDocument? Metadata { get; set; }
    public ChangeTrackingOperation Operation { get; set; }
}