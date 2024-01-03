using ToDoApp.Bll.Features.Common;
using ToDoApp.Bll.Features.Common.Mapping;

namespace ToDoApp.Bll.Features.Tasks.Create;

internal class CreateTaskHandler : IRequestHandler<CreateTaskCommand, TaskModel>
{
    private readonly ITaskDao _taskDao;
    private readonly IUser _user;
    private readonly ITaskMapper _mapper;

    public CreateTaskHandler(ITaskDao taskDao, IUser user, ITaskMapper mapper)
    {
        _taskDao = taskDao;
        _user = user;
        _mapper = mapper;
    }

    public async Task<TaskModel> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var taskDto = _mapper.MapToDto(request, DateTime.UtcNow, _user.Username);
        var taskId = await _taskDao.AddAsync(taskDto);

        return _mapper.MapToResult(taskDto, taskId);
    }
}
