using Microsoft.Extensions.DependencyInjection;
using Webion.Db.Migrations.Abstractions;

namespace Webion.Db.Migrations;

internal sealed class MigrationConfigurationBuilder : IMigrationConfigurationBuilder
{
    public MigrationConfigurationBuilder(IServiceCollection services)
    {
        Services = services;
    }

    public IServiceCollection Services { get; }
}