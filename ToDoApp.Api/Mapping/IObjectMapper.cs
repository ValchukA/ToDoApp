namespace ToDoApp.Api.Mapping;

public interface IObjectMapper
{
    CreateTaskCommand MapToCommand(CreateTaskRequest createTaskRequest);

    TaskResponse MapToResponse(TaskResult taskResult);
}
