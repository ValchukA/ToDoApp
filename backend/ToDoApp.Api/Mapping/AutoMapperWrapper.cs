using ToDoApp.Api.Features.Tasks;

namespace ToDoApp.Api.Mapping;

internal class AutoMapperWrapper : IObjectMapper
{
    private static readonly Mapper _mapper = new(new MapperConfiguration(configuration =>
    {
        configuration.CreateMap<CreateTaskRequest, CreateTaskCommand>();
        configuration.CreateMap<TaskResult, TaskResponse>();
    }));

    public CreateTaskCommand MapToCommand(CreateTaskRequest createTaskRequest) => _mapper.Map<CreateTaskCommand>(createTaskRequest);

    public TaskResponse MapToResponse(TaskResult taskResult) => _mapper.Map<TaskResponse>(taskResult);
}
