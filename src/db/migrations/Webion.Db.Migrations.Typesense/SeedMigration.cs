using Microsoft.Extensions.Options;
using Typesense;
using Webion.Db.Migrations.Abstractions;

namespace Webion.Db.Migrations.Typesense;

internal sealed class SeedMigration : IMigration
{
    private readonly ITypesenseClient _typesenseClient;
    private readonly TypesenseMigrationsOptions _options;

    public SeedMigration(ITypesenseClient typesenseClient, IOptions<TypesenseMigrationsOptions> options)
    {
        _typesenseClient = typesenseClient;
        _options = options.Value;
        Id = options.Value.SeedMigrationId;
    }

    public string Id { get; }
    
    public async Task UpAsync(CancellationToken cancellationToken)
    {
        await _typesenseClient.CreateCollection(
            schema: MigrationDoc.GetSchema(_options.CollectionName)
        );
    }

    public async Task DownAsync(CancellationToken cancellationToken)
    {
        await _typesenseClient.DeleteCollection(_options.CollectionName);
    }
}