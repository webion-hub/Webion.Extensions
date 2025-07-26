using Microsoft.Extensions.DependencyInjection;

namespace Webion.Db.Migrations.Abstractions;

public interface IMigrationConfigurationBuilder
{
    public IServiceCollection Services { get; }
}