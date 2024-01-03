using ToDoApp.Bll.Features.Tasks;
using ToDoApp.Bll.Features.Tasks.Create;

namespace ToDoApp.Bll.Features.Common.Mapping;

internal class TaskMapper : ITaskMapper
{
    public TaskModel MapToModel(CreateTaskCommand createTaskCommand, Guid taskId, string createdBy) => new()
    {
        Id = taskId,
        Title = createTaskCommand.Title,
        Description = createTaskCommand.Description,
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
