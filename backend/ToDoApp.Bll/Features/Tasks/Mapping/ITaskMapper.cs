using ToDoApp.Bll.Features.Tasks.Create;

namespace ToDoApp.Bll.Features.Tasks.Mapping;

internal interface ITaskMapper
{
    TaskModel MapToDto(CreateTaskCommand createTaskCommand, DateTime creationDateUtc, string createdBy);

    TaskModel MapToResult(CreateTaskDto createTaskDto, Guid taskId);

    TaskModel MapToResult(TaskDto taskDto);
}
