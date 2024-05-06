using System.Linq.Expressions;

namespace Webion.Extensions.Linq;

public static class SearchExtension
{
    public static IQueryable<T> SearchInsensitive<T>(this IQueryable<T> @this,
        string? query,
        Func<string, Expression<Func<T, bool>>> predicate
    )
    {
        return @this.Search(query?.ToLower(), predicate);
    }

    private static IQueryable<T> Search<T>(this IQueryable<T> @this,
        string? query,
        Func<string, Expression<Func<T, bool>>> predicate
    )
    {
        if (query is null)
            return @this;

        var tokens = query.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return tokens.Aggregate(@this, (current, token) =>
            current.Where(predicate(token))
        );
    }

}