namespace Webion.Extensions.AspNetCore.Authentication.ClickUp;

public static class ClickUpDefaults
{
    public const string AuthenticationScheme = "ClickUp";
    public const string DisplayName = "ClickUp";
    public const string AuthorizationEndpoint = "https://api.clickup.com/api";
    public const string TokenEndpoint = "https://api.clickup.com/api/v2/oauth/token";
    public const string UserInformationEndpoint = "https://api.clickup.com/api/v2/user";
}
