namespace Webion.Extensions.Linq;

public static class QueryableExtension
{
    public static IQueryable<T> If<T>(this IQueryable<T> query,
        bool condition,
        Func<IQueryable<T>, IQueryable<T>> func
    )
    {
        return condition ? func(query) : query;
    }
    
    public static IQueryable<T> If<T>(this IQueryable<T> query,
        bool condition,
        Func<IQueryable<T>, IQueryable<T>> then,
        Func<IQueryable<T>, IQueryable<T>> orElse
    )
    {
        return condition ? then(query) : orElse(query);
    }


    public static IQueryable<T> Transform<T>(this IQueryable<T> query,
        Func<IQueryable<T>, IQueryable<T>> func
    )
    {
        return func(query);
    }
}