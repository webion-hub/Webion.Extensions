using Webion.ClickUp.Api.v2.Common;

namespace Webion.ClickUp.Api.v2.Folders.Dtos;

public sealed class GetAllFoldersResponse
{
    public IEnumerable<Folders5Dto> Folders { get; init; } = [];
}