using Microsoft.EntityFrameworkCore;

namespace Webion.Extensions.EntityFrameworkCore.ChangeTracking.Abstractions;

public interface IChangeTrackingDbContext<T>
    where T : class, IChangeTrackingInfoDbo
{
    public DbSet<T> TrackingInfo { get; }
}