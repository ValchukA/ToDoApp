namespace ToDoApp.Storage.MongoDb.Options;

internal record MongoDbOptions
{
    public const string SectionKey = "MongoDb";

    public required string ConnectionString { get; init; }

    public required string DatabaseName { get; init; }
}
