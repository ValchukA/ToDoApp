namespace ToDoApp.Storage.MongoDb.Features.Tasks;

internal class TaskRepository : ITaskRepository
{
    private readonly IMongoCollection<TaskEntity> _tasksCollection;
    private readonly IObjectMapper _mapper;

    public TaskRepository(IMongoDatabase database, IObjectMapper mapper)
    {
        _tasksCollection = database.GetCollection<TaskEntity>("tasks");
        _mapper = mapper;
    }

    public async Task<TaskModel?> GetAsync(Guid taskId)
    {
        var taskEntity = await _tasksCollection
            .Find(task => task.Id == taskId)
            .FirstOrDefaultAsync();

        return taskEntity is null ? null : _mapper.MapToModel(taskEntity);
    }

    public Task CreateAsync(TaskModel taskModel)
    {
        var taskEntity = _mapper.MapToEntity(taskModel);

        return _tasksCollection.InsertOneAsync(taskEntity);
    }
}
