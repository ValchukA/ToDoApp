namespace ToDoApp.Bll.Features.Tasks;

public interface ITaskDao
{
    Task<TaskModel?> GetAsync(Guid taskId);

    Task<Guid> AddAsync(TaskModel task);
}
