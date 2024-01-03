namespace ToDoApp.Bll.Features.Common.Mapping;

internal interface IObjectMapper
{
    TaskModel MapToModel(CreateTaskCommand createTaskCommand, Guid taskId, string createdBy);
}
