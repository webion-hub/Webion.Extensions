using System.Text.Json.Serialization;

namespace Webion.Extensions.AspNetCore.Authentication.ClickUp.Model;

internal sealed class GetUserResponse
{
    public UserDto User { get; set; } = null!;

    internal sealed class UserDto
    {
        [JsonPropertyName("id")]
        public long Id { get; init; }

        [JsonPropertyName("username")]
        public string UserName { get; init; } = null!;

        [JsonPropertyName("email")]
        public string? Email { get; init; } = null!;
        
        [JsonPropertyName("color")]
        public string? Color { get; init; }

        [JsonPropertyName("profilePicture")]
        public string? ProfilePicture { get; init; }
        
        [JsonPropertyName("initials")]
        public string? Initials { get; init; } = null!;
    }
}