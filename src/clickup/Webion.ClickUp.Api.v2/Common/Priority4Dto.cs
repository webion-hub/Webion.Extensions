using System.Text.Json.Serialization;

namespace Webion.ClickUp.Api.v2.Common;

public sealed class Priority4Dto
{
    public string? Priority { get; init; }
    public string? Color { get; init; }

    [JsonPropertyName("orderindex")]
    public string? OrderIndex { get; init; }
    public string? Id { get; init; }
}