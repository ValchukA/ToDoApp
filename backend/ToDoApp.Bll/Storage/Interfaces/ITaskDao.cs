namespace ToDoApp.Bll.Storage.Interfaces;

public interface ITaskDao
{
    Task<TaskDto?> GetAsync(Guid taskId);

    Task<Guid> AddAsync(CreateTaskDto taskDto);
}
