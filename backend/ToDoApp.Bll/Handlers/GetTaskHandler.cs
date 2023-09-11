namespace ToDoApp.Bll.Handlers;

internal class GetTaskHandler : IRequestHandler<GetTaskQuery, TaskResult>
{
    private readonly ITaskDao _taskDao;
    private readonly IUser _user;
    private readonly IObjectMapper _mapper;

    public GetTaskHandler(ITaskDao taskDao, IUser user, IObjectMapper mapper)
    {
        _taskDao = taskDao;
        _user = user;
        _mapper = mapper;
    }

    public async Task<TaskResult> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var taskDto = await _taskDao.GetAsync(request.Id);

        return taskDto switch
        {
            null => throw new NotFoundException { ResourceId = request.Id },
            _ when taskDto.CreatedBy != _user.Username => throw new UnauthorizedException { ResourceId = request.Id },
            _ => _mapper.MapToResult(taskDto),
        };
    }
}
