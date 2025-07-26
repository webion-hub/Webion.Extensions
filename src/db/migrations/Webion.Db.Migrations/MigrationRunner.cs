using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Webion.Db.Migrations.Abstractions;

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
    private readonly MigrationOptions _options;

    public MigrationRunner(MigrationApplier migrationApplier, IHostApplicationLifetime hostApplicationLifetime, IOptions<MigrationOptions> options)
    {
        _migrationApplier = migrationApplier;
        _hostApplicationLifetime = hostApplicationLifetime;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _migrationApplier.ApplyAsync(_options.TargetMigrationId, stoppingToken);
        _hostApplicationLifetime.StopApplication();
    }
}