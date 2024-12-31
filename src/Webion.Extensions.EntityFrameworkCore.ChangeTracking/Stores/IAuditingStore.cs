namespace Webion.Extensions.EntityFrameworkCore.ChangeTracking.Stores;

internal interface IAuditingStore
{
    public IObservable<ChangeTrackingContext> Changes { get; }
    public void OnNext(ChangeTrackingContext context);
}