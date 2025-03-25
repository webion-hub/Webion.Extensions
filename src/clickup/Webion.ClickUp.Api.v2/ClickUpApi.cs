using System.Text.Json;
using Refit;
using Webion.ClickUp.Api.v2.Converters;
using Webion.ClickUp.Api.v2.Folders;
using Webion.ClickUp.Api.v2.Lists;
using Webion.ClickUp.Api.v2.OAuth;
using Webion.ClickUp.Api.v2.Spaces;
using Webion.ClickUp.Api.v2.Tasks;
using Webion.ClickUp.Api.v2.Team;
using Webion.ClickUp.Api.v2.Users;

namespace Webion.ClickUp.Api.v2;

internal sealed class ClickUpApi : IClickUpApi
{
    private readonly HttpClient _client;

    public HttpClient Client => _client;
    
    public IClickUpTeamApi Teams => RestService.For<IClickUpTeamApi>(_client, Settings);
    public IClickUpOAuthApi OAuth => RestService.For<IClickUpOAuthApi>(_client, Settings);
    public IClickUpUsersApi Users => RestService.For<IClickUpUsersApi>(_client, Settings);
    public IClickUpSpacesApi Spaces => RestService.For<IClickUpSpacesApi>(_client, Settings);
    public IClickUpListsApi Lists => RestService.For<IClickUpListsApi>(_client, Settings);
    public IClickUpTasksApi Tasks => RestService.For<IClickUpTasksApi>(_client, Settings);
    public IClickUpFoldersApi Folders => RestService.For<IClickUpFoldersApi>(_client, Settings);

    public ClickUpApi(HttpClient client)
    {
        _client = client;
    }

    private static readonly RefitSettings Settings = new()
    {
        ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
            Converters = { new ClickUpIdConverter() }
        }),
    };

}