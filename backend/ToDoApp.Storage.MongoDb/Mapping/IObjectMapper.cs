namespace ToDoApp.Storage.MongoDb.Mapping;

internal interface IObjectMapper
{
    TaskEntity MapToEntity(CreateTaskDto createTaskDto);

    TaskDto MapToDto(TaskEntity taskEntity);
}
