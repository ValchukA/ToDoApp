namespace ToDoApp.Bll.Handlers;

internal class GetTaskHandler : IRequestHandler<GetTaskQuery, TaskResult>
{
    private readonly ITaskDao _taskDao;
    private readonly IObjectMapper _mapper;

    public GetTaskHandler(ITaskDao taskDao, IObjectMapper mapper)
    {
        _taskDao = taskDao;
        _mapper = mapper;
    }

    public async Task<TaskResult> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var taskDto = await _taskDao.GetAsync(request.Id);

        return taskDto is null
            ? throw new NotFoundException { ResourceId = request.Id } : _mapper.MapToResult(taskDto);
    }
}
