using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Webion.Extensions.EntityFrameworkCore.ChangeTracking.Abstractions;
using Webion.Extensions.EntityFrameworkCore.ChangeTracking.Interceptors;
using Webion.Extensions.EntityFrameworkCore.ChangeTracking.Stores;

namespace Webion.Extensions.EntityFrameworkCore.ChangeTracking.Extensions;

public static class DbContextExtensions
{
    public static void AddChangeTracking<TD, TDbo>(this IServiceCollection collection)
        where TDbo: class, IChangeTrackingInfoDbo, new()
        where TD: DbContext, IChangeTrackingDbContext<TDbo>
    {
        collection.AddSingleton<IAuditingStore, AuditingStore>();
        collection.AddSingleton<IInterceptor, SaveChangesTrackingInterceptor>();
        collection.AddHostedService<ChangeTrackingService<TD, TDbo>>();
    }
}