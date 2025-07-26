using Microsoft.Extensions.Logging;
using Webion.Db.Migrations.Abstractions;

namespace Webion.Db.Migrations;

/// <summary>
/// The MigrationApplier class is responsible for managing the application and rollback
/// of database migrations. It coordinates with the migration store to determine the
/// migrations that need to be applied or rolled back and executes these operations.
/// </summary>
internal sealed class MigrationApplier
{
    private readonly IMigrationStore _migrationStore;
    private readonly IEnumerable<IMigration> _migrations;
    private readonly ILogger<MigrationApplier> _logger;

    public MigrationApplier(IMigrationStore migrationStore, ILogger<MigrationApplier> logger, IEnumerable<IMigration> migrations)
    {
        _migrationStore = migrationStore;
        _logger = logger;
        _migrations = migrations;
    }


    /// <summary>
    /// Applies database migrations up to the specified target migration ID. If a target ID is provided,
    /// it will roll back or apply migrations as necessary to reach the specified state. If no target ID
    /// is provided, all pending migrations will be applied.
    /// </summary>
    /// <param name="targetId">
    /// The ID of the target migration to which the database should be updated. If null, all pending migrations are applied.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to observe cancellation requests, allowing the operation to be cancelled if needed.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation of applying migrations.
    /// </returns>
    public async Task ApplyAsync(string? targetId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Applying migrations");
        var appliedMigrations = await _migrationStore.GetAppliedMigrationsAsync(cancellationToken);
        
        await RollbackAsync(targetId, appliedMigrations, cancellationToken);
        await ApplyAsync(targetId, appliedMigrations, cancellationToken);
    }
    
    
    private async Task ApplyAsync(
        string? targetId,
        IList<string> appliedMigrations,
        CancellationToken cancellationToken
    )
    {
        var seedMigration = await _migrationStore.GetSeedMigrationAsync(cancellationToken);
        var toApply = _migrations
            .Concat([seedMigration])
            .Order()
            .ExceptBy(appliedMigrations, x => x.Id)
            .TakeWhile(x => x.Id != targetId);

        foreach (var migration in toApply)
        {
            await migration.UpAsync(cancellationToken);
            await _migrationStore.AddMigrationAsync(migration.Id, cancellationToken);
            _logger.LogInformation("Migration {MigrationId} applied", migration.Id);
        }
    }

    private async Task RollbackAsync(
        string? targetId,
        IList<string> appliedMigrations,
        CancellationToken cancellationToken
    )
    {
        if (targetId is null)
        {
            _logger.LogInformation("No target migration specified, skipping rollback");
            return;
        }
        
        var isApplied = _migrations.Any(x => x.Id == targetId);
        if (!isApplied)
        {
            _logger.LogInformation("Target migration {TargetId} not applied, skipping rollback", targetId);
            return;
        }

        var seedMigration = await _migrationStore.GetSeedMigrationAsync(cancellationToken);
        var toRollback = _migrations
            .Concat([seedMigration])
            .ExceptBy(appliedMigrations, x => x.Id)
            .OrderDescending()
            .TakeWhile(x => x.Id != targetId);
        
        foreach (var migration in toRollback)
        {
            await migration.DownAsync(cancellationToken);
            await _migrationStore.RemoveMigrationAsync(migration.Id, cancellationToken);
            _logger.LogInformation("Migration {MigrationId} rolled back", migration.Id);
        }
    }
}