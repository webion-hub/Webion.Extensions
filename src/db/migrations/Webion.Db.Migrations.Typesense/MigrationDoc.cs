using Typesense;

namespace Webion.Db.Migrations.Typesense;

/// <summary>
/// Represents a document model for managing and storing database migration information.
/// </summary>
/// <remarks>
/// The class is used within the Typesense collection to store migration details, such as the migration identifier and timestamp.
/// It also includes utility methods for defining the schema of the collection.
/// </remarks>
internal sealed class MigrationDoc
{
    public string Id { get; set; } = string.Empty;
    public DateTimeOffset Timestamp { get; set; }


    public static Schema GetSchema(string collectionName) => new(
        name: collectionName,
        fields:
        [
            new Field("id", FieldType.String),
        ]
    );
}