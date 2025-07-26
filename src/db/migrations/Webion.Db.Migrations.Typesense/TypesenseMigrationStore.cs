using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Typesense;
using Webion.Db.Migrations.Abstractions;

namespace Webion.Db.Migrations.Typesense;

/// <summary>
/// Represents a migration store implementation using Typesense as the underlying datastore
/// to persist information about applied database migrations.
/// </summary>
/// <remarks>
/// This class interacts with the Typesense client to maintain a collection of
/// migration records. It implements the <see cref="IMigrationStore"/> interface to provide
/// operations for managing migration entries such as retrieving, adding, and removing migrations.
/// </remarks>
internal sealed class TypesenseMigrationStore : IMigrationStore
{
    private readonly ITypesenseClient _typesenseClient;
    private readonly ILogger<TypesenseMigrationStore> _logger;
    private readonly TypesenseMigrationsOptions _options;
    private readonly SeedMigration _seedMigration;
    private readonly TimeProvider _timeProvider;

    public TypesenseMigrationStore(ITypesenseClient typesenseClient, ILogger<TypesenseMigrationStore> logger, IOptions<TypesenseMigrationsOptions> options, TimeProvider timeProvider, SeedMigration seedMigration)
    {
        _typesenseClient = typesenseClient;
        _logger = logger;
        _timeProvider = timeProvider;
        _seedMigration = seedMigration;
        _options = options.Value;
    }

    public async Task<IList<string>> GetAppliedMigrationsAsync(CancellationToken cancellationToken)
    {
        var migrationsExist = await MigrationCollectionExistsAsync(cancellationToken);
        if (!migrationsExist)
            return [];
        
        var migrations = await _typesenseClient.Search<MigrationDoc>(
            collection: _options.CollectionName,
            searchParameters: new SearchParameters("*"),
            ctk: cancellationToken
        );

        return migrations.Hits
            .Select(x => x.Document.Id)
            .ToList();
    }

    public ValueTask<IMigration> GetSeedMigrationAsync(CancellationToken cancellationToken)
    {
        return new ValueTask<IMigration>(_seedMigration);
    }


    public async Task AddMigrationAsync(string migrationId, CancellationToken cancellationToken)
    {
        await _typesenseClient.UpsertDocument(_options.CollectionName, new MigrationDoc
        {
            Id = migrationId,
            Timestamp = _timeProvider.GetUtcNow(),
        });
    }

    public async Task RemoveMigrationAsync(string migrationId, CancellationToken cancellationToken)
    {
        await _typesenseClient.DeleteDocument<MigrationDoc>(_options.CollectionName, migrationId);
    }
    
    
    private async Task<bool> MigrationCollectionExistsAsync(CancellationToken cancellationToken)
    {
        try
        {
            _ = await _typesenseClient.RetrieveCollection(
                name: _options.CollectionName,
                ctk: cancellationToken
            );
            
            return true;
        }
        catch (TypesenseApiNotFoundException ex)
        {
            _logger.LogInformation(ex, "Migration collection not found");
            return false;
        }
    }

}