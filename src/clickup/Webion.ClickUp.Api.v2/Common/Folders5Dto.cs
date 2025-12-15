using System.Text.Json.Serialization;

namespace Webion.ClickUp.Api.v2.Common;

public sealed class Folders5Dto
{
    public string Id { get; init; }
    public string Name { get; init; }
    
    [JsonPropertyName("orderindex")]
    public int OrderIndex { get; init; }
    public bool OverrideStatuses { get; init; }
    public bool Hidden { get; init; }
    public Space2Dto Space { get; init; }
    public string TaskCount { get; init; }
    public bool Archived { get; init; }
    public IEnumerable<StatusDto> Statuses { get; init; }
    public IEnumerable<List4Dto> Lists { get; init; }
    public string PermissionLevel { get; init; }
}