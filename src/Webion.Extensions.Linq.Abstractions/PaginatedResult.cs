namespace Webion.Extensions.Linq.Abstractions;

public sealed record PaginatedResult<T>
{
    public required IEnumerable<T> Results { get; init; }
    public required int TotalResults { get; init; }
    public required int TotalPages { get; init; }
    public required bool HasNextPage { get; init; }
    public required bool HasPreviousPage { get; init; }
}