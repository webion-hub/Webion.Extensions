using System.Text.Json.Serialization;

namespace Webion.ClickUp.Api.v2.Common;

public sealed class ChecklistDto
{
    public string Id { get; init; }
    public string TaskId { get; init; }
    public string Name { get; init; }
    public string DateCreated { get; init; }

    [JsonPropertyName("orderindex")]
    public int OrderIndex { get; init; }
    public int Creator { get; init; }
    public int Resolved { get; init; }
    public int Unresolved { get; init; }
    public IEnumerable<object> Items { get; init; }
}