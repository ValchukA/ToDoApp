namespace ToDoApp.Storage.MongoDb.Entities;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
internal class MongoCollectionAttribute : Attribute
{
    public required string CollectionName { get; init; }
}
