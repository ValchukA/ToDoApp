using ToDoApp.Bll.Features.Common;
using ToDoApp.Bll.Features.Common.Mapping;

namespace ToDoApp.Bll.Features.Tasks.Get;

internal class GetTaskHandler : IRequestHandler<GetTaskQuery, TaskModel>
{
    private readonly ITaskDao _taskDao;
    private readonly IUser _user;
    private readonly ITaskMapper _mapper;

    public GetTaskHandler(ITaskDao taskDao, IUser user, ITaskMapper mapper)
    {
        _taskDao = taskDao;
        _user = user;
        _mapper = mapper;
    }

    public async Task<TaskModel> Handle(GetTaskQuery request, CancellationToken cancellationToken)
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
