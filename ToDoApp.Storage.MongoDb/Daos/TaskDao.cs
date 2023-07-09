namespace ToDoApp.Storage.MongoDb.Daos;

internal class TaskDao : ITaskDao
{
    private readonly IMongoCollection<TaskEntity> _tasksCollection;
    private readonly IObjectMapper<TaskDao> _mapper;

    public TaskDao(IMongoDatabase database, IObjectMapper<TaskDao> mapper)
    {
        _tasksCollection = GetCollection<TaskEntity>(database);
        _mapper = mapper;
    }

    public async Task<TaskDto?> GetAsync(Guid taskId)
    {
        var taskEntity = await _tasksCollection
            .Find(task => task.Id == taskId)
            .FirstOrDefaultAsync();

        return _mapper.Map<TaskDto>(taskEntity);
    }

    public async Task<Guid> AddAsync(CreateTaskDto taskDto)
    {
        var taskEntity = _mapper.Map<TaskEntity>(taskDto);

        await _tasksCollection.InsertOneAsync(taskEntity);

        return taskEntity.Id;
    }

    private static IMongoCollection<T> GetCollection<T>(IMongoDatabase database)
        where T : class
    {
        var attribute = typeof(T).GetCustomAttribute(typeof(MongoCollectionAttribute), true)!;

        return database.GetCollection<T>(((MongoCollectionAttribute)attribute).CollectionName);
    }
}
