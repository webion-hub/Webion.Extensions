using Webion.ClickUp.Api.v2.Common;

namespace Webion.ClickUp.Api.v2.Team.Dtos;

public sealed class GetTeamCurrentTimeEntryResponse
{
    public Datum2Dto? Data { get; init; }
}