using ToDoApp.Bll.Features.Tasks;
using ToDoApp.Bll.Features.Tasks.Create;

namespace ToDoApp.Bll.Features.Common.Mapping;

internal interface ITaskMapper
{
    TaskModel MapToModel(CreateTaskCommand createTaskCommand, Guid taskId, string createdBy);

    TaskModel MapToResult(CreateTaskDto createTaskDto, Guid taskId);

    TaskModel MapToResult(TaskDto taskDto);
}
