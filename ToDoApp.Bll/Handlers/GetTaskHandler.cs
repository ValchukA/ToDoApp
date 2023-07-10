namespace ToDoApp.Bll.Handlers;

internal class GetTaskHandler : IRequestHandler<GetTaskQuery, TaskResult>
{
    private readonly ITaskDao _taskDao;
    private readonly IObjectMapper<GetTaskHandler> _mapper;

    public GetTaskHandler(ITaskDao taskDao, IObjectMapper<GetTaskHandler> mapper)
    {
        _taskDao = taskDao;
        _mapper = mapper;
    }

    public async Task<TaskResult> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var taskDto = await _taskDao.GetAsync(request.Id);

        return taskDto is null ? throw new NotFoundException() : _mapper.Map<TaskResult>(taskDto);
    }
}
