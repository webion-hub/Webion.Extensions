using Webion.Extensions.Linq.Abstractions;

namespace Webion.Extensions.Linq;

public static class PaginationExtension
{
    public static PaginatedResult<T> Paginate<T>(this IEnumerable<T> source, 
        int page, 
        int pageSize
    )
    {
        var sourceList = source.ToList();
        var totalResults = sourceList.Count;
        var totalPages = (int)Math.Ceiling((double)totalResults / pageSize);

        var hasNextPage = page < totalPages - 1;
        var hasPreviousPage = page > 0;
        
        var results = sourceList
            .Skip(pageSize * page)
            .Take(pageSize);
        
        return new PaginatedResult<T>
        {
            Results = results,
            TotalResults = totalResults,
            TotalPages = totalPages,
            HasNextPage = hasNextPage,
            HasPreviousPage = hasPreviousPage,
        };
    }

}