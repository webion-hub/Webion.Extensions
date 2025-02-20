using Refit;
using Webion.ClickUp.Api.v2.Folders.Dtos;

namespace Webion.ClickUp.Api.v2.Folders;

public interface IClickUpFoldersApi
{
    [Get("/v2/space/{spaceId}/folder")]
    Task<GetAllFoldersResponse> GetAllAsync(string spaceId, [Query] bool? archived);
}