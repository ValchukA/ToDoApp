namespace ToDoApp.Api.Features.Common.Mapping;

public interface IObjectMapper
{
    CreateTaskCommand MapToCommand(CreateTaskRequest createTaskRequest);

    TaskResponse MapToResponse(TaskModel taskModel);
}
