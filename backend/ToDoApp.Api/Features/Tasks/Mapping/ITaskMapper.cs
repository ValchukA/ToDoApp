namespace ToDoApp.Api.Features.Tasks.Mapping;

public interface ITaskMapper
{
    CreateTaskCommand MapToCommand(CreateTaskRequest createTaskRequest);

    TaskResponse MapToResponse(TaskModel taskModel);
}
