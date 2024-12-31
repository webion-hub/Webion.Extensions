using System.Reactive.Linq;
using System.Text.Json;
using System.Threading.Channels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Webion.Extensions.EntityFrameworkCore.ChangeTracking.Abstractions;
using Webion.Extensions.EntityFrameworkCore.ChangeTracking.Stores;

namespace Webion.Extensions.EntityFrameworkCore.ChangeTracking;

internal sealed class ChangeTrackingService<TD, TDbo> : BackgroundService
    where TDbo : class, IChangeTrackingInfoDbo, new()
    where TD: DbContext, IChangeTrackingDbContext<TDbo>
{
    private readonly ILogger<ChangeTrackingService<TD, TDbo>> _logger;
    private readonly IAuditingStore _store;
    private readonly TimeProvider _timeProvider;
    private readonly IDbContextFactory<TD> _dbContextFactory;
    private readonly IEnumerable<IChangeTrackingInfoDecorator<TDbo>> _decorators;

    public ChangeTrackingService(ILogger<ChangeTrackingService<TD, TDbo>> logger, IAuditingStore store, TimeProvider timeProvider, IDbContextFactory<TD> dbContextFactory, IEnumerable<IChangeTrackingInfoDecorator<TDbo>> decorators)
    {
        _logger = logger;
        _store = store;
        _timeProvider = timeProvider;
        _dbContextFactory = dbContextFactory;
        _decorators = decorators;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await RunAsync(cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while tracking changes");
            }
        }
    }

    private async Task RunAsync(CancellationToken cancellationToken)
    {
        var channel = Channel.CreateUnbounded<IList<ChangeTrackingContext>>();
        using var _ = _store.Changes
            .Do(x => _logger.LogTrace("Update {@Update}", x))
            .Buffer(TimeSpan.FromSeconds(5))
            .Do(x => _logger.LogDebug("Handling update buffer with {Count} elements", x.Count))
            .Do(x => channel.Writer.TryWrite(x))
            .Subscribe();
            
        var chunks = channel.Reader.ReadAllAsync(cancellationToken);
        await foreach (var chunk in chunks)
        {
            await using var db = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
            var changeTrackingInfo = chunk.SelectMany(x =>
            {
                return x.Entries
                    .Where(FilterEntry)
                    .Select(e => CreateEvent(x, e));
            });

            db.TrackingInfo.AddRange(changeTrackingInfo);
            await db.SaveChangesAsync(cancellationToken);
        }
    }
    
    
    private bool FilterEntry(ChangeTrackingEntry e)
    {
        if (e.State is EntityState.Detached or EntityState.Unchanged)
        {
            _logger.LogDebug("Entity state was detached or unchanged, skipping");
            return false;
        }

        if (e.Original.Entity is IChangeTrackingInfoDbo)
        {
            _logger.LogDebug("Entity type was change tracking info, skipping");
            return false;
        }

        return true;
    }

    private TDbo CreateEvent(ChangeTrackingContext context, ChangeTrackingEntry e)
    {
        var result = new TDbo
        {
            Version = 1,
            TargetId = GetPrimaryKey(e),
            EntityName = e.Original.Metadata.ContainingEntityType.GetSchemaQualifiedTableName(),
            Timestamp = _timeProvider.GetUtcNow(),
            Metadata = JsonSerializer.SerializeToDocument(new
            {
                Properties = e.Original.Properties
                    .Where(p => p.Metadata.PropertyInfo != null)
                    .ToDictionary(p => p.Metadata.PropertyInfo!.Name, p => new
                    {
                        p.CurrentValue,
                        p.OriginalValue
                    })
            }),
            Operation = e.State switch
            {
                EntityState.Deleted => ChangeTrackingOperation.Delete,
                EntityState.Modified => ChangeTrackingOperation.Update,
                EntityState.Added => ChangeTrackingOperation.Create,
                _ => throw new ArgumentOutOfRangeException(nameof(e.State), "Not handled")
            }
        };
        
        foreach (var decorator in _decorators)
            decorator.Decorate(context, result);

        return result;
    }
    

    private static string? GetPrimaryKey(ChangeTrackingEntry e)
    {
        return e.Original.Properties
            .Where(x => x.Metadata.IsPrimaryKey())
            .Where(x => x.CurrentValue != null || x.OriginalValue != null)
            .Select(x => (x.CurrentValue ?? x.OriginalValue)?.ToString())
            .FirstOrDefault();
    }
}