Adds the `IJobConfig` interface, that can be used to configure quartz jobs separately.

## Example usage
```csharp
public sealed class CleanRefreshTokensJob : IJobConfig
{
    public void Configure(IServiceCollectionQuartzConfigurator configurator)
    {
        configurator.AddJob<SyncPaylineJob>(CleanRefreshTokensJob.JobKey, options =>
        {
            options.StoreDurably();
            options.WithIdentity(CleanRefreshTokensJob.JobKey);
            options.WithDescription("Cleans expired refresh tokens from the database");
            options.DisallowConcurrentExecution();
        });
        
        configurator.AddTrigger(t => t
            .ForJob(CleanRefreshTokensJob.JobKey)
            .WithIdentity(CleanRefreshTokensJob.TriggerKey)
            .WithDescription("Daily execution at midnight")
            .WithCronSchedule(CronScheduleBuilder.DailyAtHourAndMinute(00, 00))
        );
    }
}

// Program.cs
builder.Services.AddQuartz(q =>
{
    q.Configure<CleanRefreshTokensJob>();
    
    // ...
});
```