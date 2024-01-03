namespace ToDoApp.Bll.Features.Tasks;

public interface ITaskRepository
{
    Task<TaskModel?> GetAsync(Guid taskId);

    Task CreateAsync(TaskModel taskModel);
}
