using ToDoApp.Api.Features.Tasks.Contracts;

namespace ToDoApp.Api.Features.Tasks.Mapping;

public interface IObjectMapper
{
    CreateTaskCommand MapToCommand(CreateTaskRequest createTaskRequest);

    TaskResponse MapToResponse(TaskResult taskResult);
}
