namespace Webion.ClickUp.Api.v2.Common;

public sealed class SharingDto
{
    public bool Public { get; init; }
    public bool? PublicShareExpiresOn { get; init; }
    public IEnumerable<string> PublicFields { get; init; }
    public object? Token { get; init; }
    public bool SeoOptimized { get; init; }
}