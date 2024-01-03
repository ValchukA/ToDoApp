using ToDoApp.Bll.Features.Tasks;

namespace ToDoApp.Storage.MongoDb.Daos;

internal class TaskDao : ITaskDao
{
    private readonly IMongoCollection<TaskEntity> _tasksCollection;
    private readonly IObjectMapper _mapper;

    public TaskDao(IMongoDatabase database, IObjectMapper mapper)
    {
        _tasksCollection = GetCollection<TaskEntity>(database);
        _mapper = mapper;
    }

    public async Task<TaskDto?> GetAsync(Guid taskId)
    {
        var taskEntity = await _tasksCollection
            .Find(task => task.Id == taskId)
            .FirstOrDefaultAsync();

        return taskEntity is null ? null : _mapper.MapToDto(taskEntity);
    }

    public async Task<Guid> AddAsync(CreateTaskDto taskDto)
    {
        var taskEntity = _mapper.MapToEntity(taskDto);
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
