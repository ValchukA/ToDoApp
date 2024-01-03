namespace ToDoApp.Bll.Features.Tasks.Create;

internal class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskModel>
{
    private readonly ITaskRepository _taskDao;
    private readonly IUser _user;
    private readonly IObjectMapper _mapper;

    public CreateTaskHandler(ITaskRepository taskDao, IUser user, IObjectMapper mapper)
    {
        _taskDao = taskDao;
        _user = user;
        _mapper = mapper;
    }

    public async Task<TaskModel> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var taskModel = _mapper.MapToModel(request, Guid.NewGuid(), _user.Username);
        await _taskDao.CreateAsync(taskModel);

        return taskModel;
    }
}
