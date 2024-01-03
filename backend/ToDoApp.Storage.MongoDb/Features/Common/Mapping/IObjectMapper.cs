namespace ToDoApp.Storage.MongoDb.Features.Common.Mapping;

internal interface IObjectMapper
{
    TaskEntity MapToEntity(TaskModel taskModel);

    TaskModel MapToModel(TaskEntity taskEntity);
}
