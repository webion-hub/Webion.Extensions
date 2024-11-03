using Quartz;

namespace Webion.Extensions.Quartz.DependencyInjection;

public interface IJobConfig
{
    public void Configure(IServiceCollectionQuartzConfigurator configurator);
}