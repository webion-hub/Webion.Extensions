using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Webion.Extensions.EntityFrameworkCore.ChangeTracking.Abstractions;
using Webion.Extensions.EntityFrameworkCore.ChangeTracking.Stores;

namespace Webion.Extensions.EntityFrameworkCore.ChangeTracking.Interceptors;

internal sealed class SaveChangesTrackingInterceptor : ISaveChangesInterceptor
{
    private readonly IAuditingStore _store;
    private readonly ILogger<SaveChangesTrackingInterceptor> _logger;

    public SaveChangesTrackingInterceptor(IAuditingStore store, ILogger<SaveChangesTrackingInterceptor> logger)
    {
        _store = store;
        _logger = logger;
    }
    
    public InterceptionResult<int> SavingChanges(DbContextEventData e, InterceptionResult<int> result)
    {
        var entries = e.Context?.ChangeTracker
            .Entries()
            .Where(x => x.Entity is not IChangeTrackingInfoDbo)
            .Select(x => new ChangeTrackingEntry(x, x.State))
            .ToList() ?? [];
        
        if (entries.Count == 0)
        {
            _logger.LogDebug("No entries, skipping");
            return result;
        }
        
        _logger.LogDebug("Handling {Count} entries", entries.Count);
        
        var changeTrackingData = new ChangeTrackingContext
        {
            DbContext = e.Context,
            Entries = entries,
            EventId = e.EventId,
            EventIdCode = e.EventIdCode
        };
        
        _store.OnNext(changeTrackingData);
        return result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default
    )
    {
        return ValueTask.FromResult(SavingChanges(eventData, result));
    }
}