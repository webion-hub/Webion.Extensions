using System.Text.Json.Serialization;
using Webion.ClickUp.Api.v2.Converters;

namespace Webion.ClickUp.Api.v2.Common;

public sealed class Task9Dto
{
    public string Id { get; init; }
    public string CustomId { get; init; }
    public int CustomItemId { get; init; }
    public string Name { get; init; }
    public string TextContent { get; init; }
    public string Description { get; init; }
    public Status11Dto Status { get; init; }
    public string? OrderIndex { get; init; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTimeOffset DateCreated { get; init; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTimeOffset DateUpdated { get; init; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTimeOffset DateClosed { get; init; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTimeOffset DateDone { get; init; }
    public CreatorDto Creator { get; init; }
    public IEnumerable<User2Dto> Assignees { get; init; }
    public IEnumerable<User2Dto> Watchers { get; init; }
    public IEnumerable<ChecklistDto> Checklists { get; init; }
    public IEnumerable<TagDto> Tags { get; init; }
    public Task9Dto? Parent { get; init; }
    public Priority4Dto? Priority { get; init; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTimeOffset? DueDate { get; init; }

    [JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTimeOffset? StartDate { get; init; }
    public object? Point { get; init; }
    public int? TimeEstimate { get; init; }
    public int? TimeSpent { get; init; }
    public IEnumerable<object> CustomFields { get; init; }
    public IEnumerable<object> Dependencies { get; init; }
    public IEnumerable<object> LinkedTasks { get; init; }
    public IEnumerable<object> Locations { get; init; }
    public string TeamId { get; init; }
    public string Url { get; init; }
    public SharingDto Sharing { get; init; }
    public string PermissionLevel { get; init; }
    public ListDto List { get; init; }
    public ProjectDto Project { get; init; }
    public FolderDto Folder { get; init; }
    public SpaceDto Space { get; init; }
}