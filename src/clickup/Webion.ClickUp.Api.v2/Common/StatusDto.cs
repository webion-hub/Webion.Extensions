using System.Text.Json.Serialization;

namespace Webion.ClickUp.Api.v2.Common;

public sealed class StatusDto
{
    public string? Id { get; init; }
    public required string Status { get; init; }
    public required string Color { get; init; }

    [JsonPropertyName("orderindex")]
    public int OrderIndex { get; init; }
    public required string Type { get; init; }
    public string? StatusGroup { get; init; }
}