namespace ToDoApp.Bll.Handlers;

internal class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskResult>
{
    private readonly ITaskDao _taskDao;
    private readonly IObjectMapper _mapper;

    public CreateTaskHandler(ITaskDao taskDao, IObjectMapper mapper)
    {
        _taskDao = taskDao;
        _mapper = mapper;
    }

    public async Task<TaskResult> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var taskDto = _mapper.MapToDto(request, DateTime.UtcNow);
        var taskId = await _taskDao.AddAsync(taskDto);

        return _mapper.MapToResult(taskDto, taskId);
    }
}
