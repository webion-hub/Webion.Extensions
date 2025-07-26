namespace Webion.Db.Migrations.Abstractions;

public interface IMigrationStore
{
    /// <summary>
    /// Retrieves the list of applied migrations from the database.
    /// </summary>
    /// <param name="cancellationToken">
    /// A token to observe cancellation requests, used to cancel the operation if necessary.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains an enumerable of strings representing the names of the applied migrations.
    /// </returns>
    Task<IList<string>> GetAppliedMigrationsAsync(CancellationToken cancellationToken);


    ValueTask<IMigration> GetSeedMigrationAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Adds a migration to the store, marking it as applied.
    /// </summary>
    /// <param name="migrationId">
    /// The unique identifier of the migration to be added.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to observe cancellation requests, used to cancel the operation if necessary.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    Task AddMigrationAsync(string migrationId, CancellationToken cancellationToken);

    /// <summary>
    /// Removes a migration entry from the database.
    /// </summary>
    /// <param name="migrationId">
    /// The identifier of the migration to be removed.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to observe cancellation requests, used to cancel the operation if necessary.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// </returns>
    Task RemoveMigrationAsync(string migrationId, CancellationToken cancellationToken);
}