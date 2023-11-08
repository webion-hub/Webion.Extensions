using Webion.Extensions.Linq.Abstractions;

namespace Webion.Extensions.Linq;

public static class CollectionExtensions
{
    public static ReplaceResult<T1, T2> SoftReplace<T1, T2>(this ICollection<T1> collection, 
        IEnumerable<T2> replacement,
        Func<T1, T2, bool> match,
        Func<T2, T1> add,
        Action<T1, T2> update,
        Action<T1> delete
    )
    {
        var newItems = replacement.ToList();
        var removed = new List<T1>();
        
        foreach (var existingItem in collection)
        {
            var newItem = newItems.FirstOrDefault(n => match(existingItem, n));
            if (newItem != null)
            {
                update(existingItem, newItem);
            }
            else
            {
                removed.Add(existingItem);
            }
        }

        foreach (var newItem in newItems)
        {
            var existingItem = collection.FirstOrDefault(s => match(s, newItem));
            if (existingItem != null)
                continue;
            
            var addedItem = add(newItem);
            collection.Add(addedItem);
        }
        
        foreach (var d in removed)
            delete(d);

        return new ReplaceResult<T1, T2>(removed);
    }
}