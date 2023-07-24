namespace ToDoApp.Storage.MongoDb.Mapping;

[Mapper]
internal partial class MapperlyMapper : IObjectMapper
{
    [MapperIgnoreTarget(nameof(TaskEntity.Id))]
    public partial TaskEntity MapToEntity(CreateTaskDto createTaskDto);

    [MapperIgnoreSource(nameof(TaskEntity.CreationDateUtc))]
    public partial TaskDto MapToDto(TaskEntity taskEntity);
}
