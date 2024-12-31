namespace Webion.Extensions.EntityFrameworkCore.ChangeTracking.Abstractions;

internal interface IChangeTrackingInfoDecorator<in TDbo>
    where TDbo : class, IChangeTrackingInfoDbo, new()
{
    void Decorate(ChangeTrackingContext context, TDbo dbo);
}