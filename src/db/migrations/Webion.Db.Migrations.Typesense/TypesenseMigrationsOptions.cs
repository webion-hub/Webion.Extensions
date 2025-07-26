namespace Webion.Db.Migrations.Typesense;

internal class TypesenseMigrationsOptions
{
    /// <summary>
    /// Gets or sets the name of the collection used to store migration documents in the Typesense database.
    /// </summary>
    /// <remarks>
    /// This property is utilized to identify the specific collection within the Typesense database
    /// where migration-related data is stored and managed.
    /// </remarks>
    public string CollectionName { get; set; } = "migrations";

    /// <summary>
    /// Gets or sets the unique identifier for the seed migration used to initialize the database.
    /// </summary>
    /// <remarks>
    /// This property defines a predefined identifier for the seed migration, which is executed to set up
    /// the initial state of the database schema or data when no migrations have been previously applied.
    /// </remarks>
    public string SeedMigrationId { get; set; } = "000000000000";
}