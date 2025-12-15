namespace Webion.ClickUp.Api.v2.Common;

public sealed class List4Dto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public int OrderIndex { get; init; }
    public string? Content { get; init; }
    public Status11Dto Status { get; init; }
    public Priority4Dto Priority { get; init; }
    public string? Assegnee { get; init; }
    public int TaskCount { get; init; }
    public string DueDate { get; init; }
    public string StartDate { get; init; }
    public Folder3Dto? Folder { get; init; }
    public Space2Dto Space { get; init; }
    public bool Archived { get; init; }
    public bool? OverrideStatuses { get; init; }
    public IEnumerable<StatusDto>? Statuses { get; init; }
    public string PermissionLevel { get; init; }
}