namespace ToDoApp.Api.Features.Tasks.Mapping;

internal class TaskAutoMapper : ITaskMapper
{
    private static readonly Mapper _mapper = new(new MapperConfiguration(configuration =>
    {
        configuration.CreateMap<CreateTaskRequest, CreateTaskCommand>();
        configuration.CreateMap<TaskModel, TaskResponse>();
    }));

    public CreateTaskCommand MapToCommand(CreateTaskRequest createTaskRequest) => _mapper.Map<CreateTaskCommand>(createTaskRequest);

    public TaskResponse MapToResponse(TaskModel taskModel) => _mapper.Map<TaskResponse>(taskModel);
}
