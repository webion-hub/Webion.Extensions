namespace Webion.ClickUp.Api.v2.Common;

public sealed class ProjectDto
{
    public required string Id { get; init; }
    public required string Name { get; init; }
    public required bool Hidden { get; init; }
    public required bool Access { get; init; }
}