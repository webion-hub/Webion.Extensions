using Webion.ClickUp.Api.v2.Common;

namespace Webion.ClickUp.Api.v2.Lists.Dtos;

public sealed class GetAllListsResponse
{
    public required IEnumerable<List4Dto> Lists { get; init; }
}