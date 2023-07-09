namespace ToDoApp.Bll.Handlers;

internal class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskResult>
{
    private readonly ITaskDao _taskDao;
    private readonly IObjectMapper<CreateTaskHandler> _mapper;

    public CreateTaskHandler(ITaskDao taskDao, IObjectMapper<CreateTaskHandler> mapper)
    {
        _taskDao = taskDao;
        _mapper = mapper;
    }

    public async Task<TaskResult> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var taskDto = _mapper.Map<CreateTaskDto>(request) with { CreationDateUtc = DateTime.UtcNow };

        var taskId = await _taskDao.AddAsync(taskDto);

        return _mapper.Map<TaskResult>(taskDto) with { Id = taskId };
    }
}
