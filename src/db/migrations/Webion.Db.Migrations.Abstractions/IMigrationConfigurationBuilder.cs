using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Webion.Db.Migrations.Abstractions;

public interface IMigrationConfigurationBuilder
{
    public IServiceCollection Services { get; }

    /// <summary>
    /// Registers all migrations from the assembly that contains the specified type.
    /// </summary>
    /// <typeparam name="T">
    /// A type used to determine the assembly from which migrations should be loaded.
    /// </typeparam>
    /// <returns>
    /// The current instance of <see cref="IMigrationConfigurationBuilder"/> for method chaining.
    /// </returns>
    IMigrationConfigurationBuilder AddMigrationsFromAssemblyContaining<T>();

    /// <summary>
    /// Registers all migrations from the specified assembly.
    /// </summary>
    /// <param name="assembly">
    /// The assembly from which migrations should be loaded.
    /// </param>
    /// <returns>
    /// The current instance of <see cref="IMigrationConfigurationBuilder"/> for method chaining.
    /// </returns>
    IMigrationConfigurationBuilder AddMigrationsFromAssembly(Assembly assembly);
}