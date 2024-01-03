namespace ToDoApp.Storage.MongoDb.Entities;

[MongoCollection(CollectionName = "tasks")]
internal record TaskEntity
{
    public Guid Id { get; init; }

    public required string Title { get; init; }

    public string? Description { get; init; }

    public required string CreatedBy { get; init; }
}
