using Quartz;

namespace Webion.Extensions.Quartz.DependencyInjection;

public static class QuartzJobConfig
{
    public static void Configure<TJobConfig>(this IServiceCollectionQuartzConfigurator configurator)
        where TJobConfig: IJobConfig, new()
    {
        new TJobConfig().Configure(configurator);
    }
}