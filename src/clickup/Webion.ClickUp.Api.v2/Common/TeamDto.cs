namespace Webion.ClickUp.Api.v2.Common;

public sealed class TeamDto
{
    public ClickUpId Id { get; init; }
    public string Name { get; init; }
    public string Color { get; init; }
    public string Avatar { get; init; }
    public ICollection<MemberDto> Members { get; init; }
}