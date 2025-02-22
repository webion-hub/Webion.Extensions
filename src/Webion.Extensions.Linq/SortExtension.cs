using System.Linq.Expressions;
using Webion.Extensions.Linq.Abstractions;

namespace Webion.Extensions.Linq;

public static class SortExtension
{
    /// <summary>
    /// Sorts an <see cref="IQueryable{T}"/> based on the specified sorting criteria.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the queryable collection.</typeparam>
    /// <typeparam name="TField">The enum type representing sort fields.</typeparam>
    /// <typeparam name="TKey">The type of the key used for sorting.</typeparam>
    /// <param name="query">The queryable collection to sort.</param>
    /// <param name="sortBy">An array of sorting criteria, each specifying a field and direction.</param>
    /// <param name="selector">A function that maps a sort field to an <see cref="Expression{TDelegate}"/> specifying the property to sort by.</param>
    /// <returns>A sorted queryable collection based on the provided criteria. If no sorting criteria are specified, the original query is returned unchanged.</returns>
    public static IQueryable<T> Sort<T, TField>(this IQueryable<T> query,
        SortByFilter<TField>[]? sortBy,
        Func<SortByFilter<TField>, Func<IQueryable<T>, IQueryable<T>>> selector
    )
        where T : class
        where TField : Enum
    {
        if (sortBy is null)
            return query;

        if (sortBy.Length == 0)
            return query;

        return sortBy.Aggregate(query, (current, sort) => selector(sort)(current));
    }
}