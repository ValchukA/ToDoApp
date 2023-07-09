namespace ToDoApp.Storage.MongoDb.Options;

public record MongoDbOptions
{
    public const string SectionKey = "MongoDb";

    public required string ConnectionString { get; init; }

    public required string DatabaseName { get; init; }
}
