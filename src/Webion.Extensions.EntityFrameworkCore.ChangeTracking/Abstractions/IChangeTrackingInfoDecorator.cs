namespace Webion.Extensions.EntityFrameworkCore.ChangeTracking.Abstractions;

public interface IChangeTrackingInfoDecorator<in TDbo>
    where TDbo : class, IChangeTrackingInfoDbo, new()
{
    void Decorate(ChangeTrackingContext context, TDbo dbo);
}