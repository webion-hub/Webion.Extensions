using Webion.ClickUp.Api.v2.Folders;
using Webion.ClickUp.Api.v2.Lists;
using Webion.ClickUp.Api.v2.OAuth;
using Webion.ClickUp.Api.v2.Spaces;
using Webion.ClickUp.Api.v2.Tasks;
using Webion.ClickUp.Api.v2.Team;
using Webion.ClickUp.Api.v2.Users;

namespace Webion.ClickUp.Api.v2;

public interface IClickUpApi
{
    /// <summary>
    /// The http client used for the underlying calls.
    /// </summary>
    public HttpClient Client { get; }
    
    public IClickUpTeamApi Teams { get; }
    public IClickUpOAuthApi OAuth { get; }
    public IClickUpUsersApi Users { get; }
    public IClickUpSpacesApi Spaces { get; }
    public IClickUpListsApi Lists { get; }
    public IClickUpTasksApi Tasks { get; }
    public IClickUpFoldersApi Folders { get; }
}