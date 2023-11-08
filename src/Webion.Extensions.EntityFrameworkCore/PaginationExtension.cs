using Microsoft.EntityFrameworkCore;
using Webion.Extensions.Linq.Abstractions;

namespace Webion.Extensions.EntityFrameworkCore;

public static class PaginationExtension
{
    public static async Task<PaginatedResult<T>> PaginateAsync<T>(this IQueryable<T> query,
        int page,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        var totalResults = await query.CountAsync(cancellationToken);
        var totalPages = (int)Math.Ceiling((double)totalResults / pageSize);

        var hasNextPage = page < totalPages - 1;
        var hasPreviousPage = page > 0;

        var results = await query
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<T>
        {
            Results = results,
            TotalResults = totalResults,
            TotalPages = totalPages,
            HasNextPage = hasNextPage,
            HasPreviousPage = hasPreviousPage
        };
    }
}