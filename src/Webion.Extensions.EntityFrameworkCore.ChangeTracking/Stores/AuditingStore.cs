using System.Reactive.Subjects;

namespace Webion.Extensions.EntityFrameworkCore.ChangeTracking.Stores;

internal sealed class AuditingStore : IAuditingStore
{
    private readonly Subject<ChangeTrackingContext> _subject = new();

    public IObservable<ChangeTrackingContext> Changes => _subject;

    public void OnNext(ChangeTrackingContext context)
    {
        _subject.OnNext(context);
    }
}