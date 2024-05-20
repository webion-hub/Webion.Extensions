using Webion.Extensions.Linq.Abstractions;

namespace Webion.Extensions.Linq;

public static class CollectionExtensions
{
    /// <summary>
    /// Soft replaces the elements in the collection with the specified replacement collection based on a matching criterion.
    /// </summary>
    /// <remarks>
    /// If <c>skipAdd</c>, <c>skipUpdate</c> or <c>skipDelete</c> are used, the method will not loop during the given phase,
    /// meaning that the corresponding lists inside the result will be empty.
    /// </remarks>
    /// <typeparam name="T1">The type of the elements in the original collection.</typeparam>
    /// <typeparam name="T2">The type of the elements in the replacement collection.</typeparam>
    /// <param name="collection">The collection to soft replace the elements in.</param>
    /// <param name="replacement">The replacement collection.</param>
    /// <param name="match">The matching criterion used to determine whether an element in the original collection should be replaced.</param>
    /// <param name="add">
    /// The function that is invoked to add a new element to the collection. <br/>
    /// If set to <c>null</c>, the <see cref="ReplaceResult{T1,T2}.Created"/> property of the result will be empty, as no mapping occurs.<br/>
    /// Defaults to <c>null</c>.
    /// </param>
    /// <param name="update">The action that is invoked to update an existing element in the collection. Defaults to <c>null</c>.</param>
    /// <param name="delete">The action that is invoked to delete a removed element from the collection. Defaults to <c>null</c>.</param>
    /// <param name="skipAdd">
    /// Indicates whether to skip adding new elements to the collection.<br/>
    /// If set to <c>false</c>, the <see cref="ReplaceResult{T1,T2}.Added"/> and <see cref="ReplaceResult{T1,T2}.Created"/> properties
    /// inside the result will be left empty.<br/>
    /// Defaults to <c>false</c>
    /// </param>
    /// <param name="skipUpdate">
    /// Indicates whether to skip updating existing elements in the collection.<br/>
    /// If set to <c>false</c>, the <see cref="ReplaceResult{T1,T2}.Updated"/> property inside the result will be left empty.<br/>
    /// Defaults to <c>false</c>
    /// </param>
    /// <param name="skipDelete">
    /// Indicates whether to skip deleting removed elements from the collection.<br/>
    /// If set to <c>false</c>, the <see cref="ReplaceResult{T1,T2}.Removed"/> property inside the result will be left empty.<br/>
    /// Defaults to <c>false</c>
    /// </param>
    /// <returns>A <see cref="ReplaceResult{T1, T2}"/> object that represents the result of the soft replace operation.</returns>
    public static ReplaceResult<T1, T2> SoftReplace<T1, T2>(this ICollection<T1> collection, 
        IEnumerable<T2> replacement,
        Func<T1, T2, bool> match,
        Func<T2, T1>? add = default,
        Action<T1, T2>? update = default,
        Action<T1>? delete = default,
        bool skipAdd = false,
        bool skipUpdate = false,
        bool skipDelete = false
    )
    where T1: class
    where T2: class 
    {
        var newItems = replacement.ToList();
        var deleted = new List<T1>();
        var created = new List<T1>();
        var updated = new List<T1>();
        var added = new List<T2>();
        
        foreach (var existingItem in collection)
        {
            var newItem = newItems.FirstOrDefault(n => match(existingItem, n));
            if (newItem is not null)
            {
                if (skipUpdate)
                    continue;
                
                update?.Invoke(existingItem, newItem);
                updated.Add(existingItem);
            }
            else if (!skipDelete)
            {
                deleted.Add(existingItem);
            }
        }

        if (!skipAdd)
        {
            foreach (var newItem in newItems)
            {
                var existingItem = collection.FirstOrDefault(s => match(s, newItem));
                if (existingItem is not null)
                    continue;

                added.Add(newItem);
                if (add is not null)
                {
                    var addedItem = add(newItem);
                    collection.Add(addedItem);
                    created.Add(addedItem);
                }
            }
        }

        if (!skipDelete && delete is not null)
        {
            foreach (var d in deleted)
                delete(d);
        }

        return new ReplaceResult<T1, T2>
        {
            Added = added,
            Created = created,
            Updated = updated,
            Removed = deleted,
        };
    }
}