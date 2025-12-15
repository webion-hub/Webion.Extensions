using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Webion.ClickUp.Api.v2.Common;

[DebuggerDisplay("{Email}")]
public sealed class User2Dto
{
    public ClickUpId Id { get; init; }
    public string Username { get; init; }
    public string Initials { get; init; }
    public string Email { get; init; }
    public string Color { get; init; }

    [JsonPropertyName("profilePicture")]
    public string ProfilePicture { get; init; }
}