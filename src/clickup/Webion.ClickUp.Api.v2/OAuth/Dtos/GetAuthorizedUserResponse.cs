using Webion.ClickUp.Api.v2.Common;

namespace Webion.ClickUp.Api.v2.OAuth.Dtos;

public sealed class GetAuthorizedUserResponse
{
    public required ClickUpUserDto User { get; init; }
}