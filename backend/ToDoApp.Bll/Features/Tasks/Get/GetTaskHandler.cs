namespace ToDoApp.Bll.Features.Tasks.Get;

internal class GetTaskHandler : IRequestHandler<GetTaskQuery, TaskModel>
{
    private readonly ITaskRepository _taskDao;
    private readonly IUser _user;

    public GetTaskHandler(ITaskRepository taskDao, IUser user)
    {
        _taskDao = taskDao;
        _user = user;
    }

    public async Task<TaskModel> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var taskModel = await _taskDao.GetAsync(request.Id) ?? throw new NotFoundException { ResourceId = request.Id };

        return taskModel.CreatedBy != _user.Username
            ? throw new UnauthorizedException { ResourceId = request.Id } : taskModel;
    }
}
