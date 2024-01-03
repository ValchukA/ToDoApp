using ToDoApp.Bll.Features.Tasks;
using ToDoApp.Bll.Features.Tasks.Create;

namespace ToDoApp.Bll.Features.Tasks.Mapping;

internal class TaskMapper : ITaskMapper
{
    public CreateTaskDto MapToDto(CreateTaskCommand createTaskCommand, DateTime creationDateUtc, string createdBy) => new()
    {
        Title = createTaskCommand.Title,
        Description = createTaskCommand.Description,
        CreationDateUtc = creationDateUtc,
        CreatedBy = createdBy,
    };

    public TaskModel MapToResult(CreateTaskDto createTaskDto, Guid taskId) => new()
    {
        Id = taskId,
        Title = createTaskDto.Title,
        Description = createTaskDto.Description,
    };

    public TaskModel MapToResult(TaskDto taskDto) => new()
    {
        Id = taskDto.Id,
        Title = taskDto.Title,
        Description = taskDto.Description,
    };
}
