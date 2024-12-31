using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace Webion.Extensions.EntityFrameworkCore.ChangeTracking;

/// <summary>
/// Represents the context for change tracking operations within the DbContext.
/// Used to capture and store metadata about the changes being tracked in the Entity Framework context.
/// </summary>
public sealed class ChangeTrackingContext
{
    public required DbContext? DbContext { get; init; }
    public required IList<ChangeTrackingEntry> Entries { get; init; }
    public required EventId EventId { get; init; }
    public required string EventIdCode { get; init; }
}

public sealed record ChangeTrackingEntry(
    EntityEntry Original,
    EntityState State
);