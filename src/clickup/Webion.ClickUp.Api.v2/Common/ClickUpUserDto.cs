using System.Text.Json.Serialization;

namespace Webion.ClickUp.Api.v2.Common;

public sealed class ClickUpUserDto
{
    public ClickUpId Id { get; init; }

    [JsonPropertyName("username")]
    public string UserName { get; init; }
    public string Color { get; init; }

    [JsonPropertyName("profilePicture")]
    public string ProfilePicture { get; init; }
    public string? Email { get; init; }
}