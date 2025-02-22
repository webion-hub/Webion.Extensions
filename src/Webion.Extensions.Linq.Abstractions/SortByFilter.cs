namespace Webion.Extensions.Linq.Abstractions;

/// <summary>
/// Represents a filter for sorting operations with defined direction and field to sort by.
/// </summary>
/// <typeparam name="TField">
/// The enum type representing the fields available for sorting.
/// </typeparam>
public sealed class SortByFilter<TField> where TField : Enum
{
    /// <summary>
    /// Gets or sets the direction of the sorting operation, which specifies whether the sorting
    /// is in ascending or descending order.
    /// </summary>
    public required SortDirection Direction { get; init; }

    /// <summary>
    /// Gets or sets the field used for the sorting operation.
    /// This specifies the property or attribute to sort by.
    /// </summary>
    public required TField Field { get; init; }
}