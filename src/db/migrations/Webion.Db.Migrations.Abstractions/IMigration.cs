namespace Webion.Db.Migrations.Abstractions;

/// <summary>
/// Represents a database migration that can be applied or rolled back.
/// </summary>
public interface IMigration
{
    /// <summary>
    /// Gets the unique identifier of the migration.
    /// This identifier is used to track and apply specific migrations in the database migration process.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Applies the operations necessary to migrate the database schema to a newer state defined by this migration.
    /// </summary>
    /// <param name="cancellationToken">
    /// A token that can be used to propagate notification that the operation should be canceled.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation of applying the migration.
    /// </returns>
    Task UpAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Reverts the operations applied by this migration, rolling back the database schema to its previous state.
    /// </summary>
    /// <param name="cancellationToken">
    /// A token that can be used to propagate notification that the operation should be canceled.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation of rolling back the migration.
    /// </returns>
    Task DownAsync(CancellationToken cancellationToken);
}