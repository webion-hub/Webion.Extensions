namespace Webion.Extensions.Linq.Abstractions;

/// <summary>
/// Represents the result of a soft replace operation.
/// </summary>
/// <typeparam name="T1">The type of the elements in the original collection.</typeparam>
/// <typeparam name="T2">The type of the elements in the replacement collection.</typeparam>
public sealed class ReplaceResult<T1, T2>
{
    /// <summary>
    /// Gets the list of items that were added during the replace operation.
    /// </summary>
    /// <typeparam name="T2">The type of the elements in the replacement collection.</typeparam>
    /// <value>
    /// The list of added items.
    /// </value>
    public required List<T2> Added { get; init; }

    /// <summary>
    /// Gets the list of items that were created during the replace operation.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the original collection.</typeparam>
    /// <value>
    /// The list of created items.
    /// </value>
    public required List<T1> Created { get; init; }


    /// <summary>
    /// Gets the list of items that were updated during the replace operation.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the original collection.</typeparam>
    /// <value>
    /// The list of updated items.
    /// </value>
    public required List<T1> Updated { get; init; }


    /// <summary>
    /// Gets the list of items that were removed during the replace operation.
    /// </summary>
    /// <typeparam name="T1">The type of the elements in the original collection.</typeparam>
    /// <value>
    /// The list of removed items.
    /// </value>
    public required List<T1> Removed { get; init; }
}