using Microsoft.Extensions.Hosting;

namespace Webion.Db.Migrations;

/// <summary>
/// The MigrationRunner class is responsible for managing the execution of database migrations
/// during the application startup phase. It is implemented as a background service that applies
/// outstanding database migrations and stops the application once migrations are applied.
/// </summary>
internal sealed class MigrationRunner : BackgroundService
{
    private readonly MigrationApplier _migrationApplier;
    private readonly IHostApplicationLifetime _hostApplicationLifetime;

    public MigrationRunner(MigrationApplier migrationApplier, IHostApplicationLifetime hostApplicationLifetime)
    {
        _migrationApplier = migrationApplier;
        _hostApplicationLifetime = hostApplicationLifetime;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _migrationApplier.ApplyAsync(null, stoppingToken);
        _hostApplicationLifetime.StopApplication();
    }
}