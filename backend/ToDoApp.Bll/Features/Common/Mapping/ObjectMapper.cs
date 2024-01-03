namespace ToDoApp.Bll.Features.Common.Mapping;

internal class ObjectMapper : IObjectMapper
{
    public TaskModel MapToModel(CreateTaskCommand createTaskCommand, Guid taskId, string createdBy) => new()
    {
        Id = taskId,
        Title = createTaskCommand.Title,
        Description = createTaskCommand.Description,
        CreatedBy = createdBy,
    };
}
