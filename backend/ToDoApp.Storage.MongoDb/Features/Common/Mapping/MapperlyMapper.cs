namespace ToDoApp.Storage.MongoDb.Mapping;

[Mapper]
internal partial class MapperlyMapper : IObjectMapper
{
    public partial TaskEntity MapToEntity(TaskModel taskModel);

    public partial TaskModel MapToModel(TaskEntity taskEntity);
}
