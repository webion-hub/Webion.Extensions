namespace Webion.ClickUp.Api.v2.Common;

public sealed class TagDto
{
    public string Name { get; init; }
    public string TagFg { get; init; }
    public string TagBg { get; init; }
    public int? Creator { get; init; }
}