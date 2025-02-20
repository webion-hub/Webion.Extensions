using Webion.ClickUp.Api.v2.Common;

namespace Webion.ClickUp.Api.v2.Team.Dtos;

public sealed class GetTeamsResponse
{
    public required IEnumerable<TeamDto> Teams { get; init; } = [];
}