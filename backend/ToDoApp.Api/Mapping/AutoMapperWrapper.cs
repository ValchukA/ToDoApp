namespace ToDoApp.Api.Mapping;

internal class AutoMapperWrapper : IObjectMapper
{
    private static readonly Mapper _mapper = new(new MapperConfiguration(configurationExpression =>
    {
        configurationExpression.CreateMap<CreateTaskRequest, CreateTaskCommand>();
        configurationExpression.CreateMap<TaskResult, TaskResponse>();
    }));

    public CreateTaskCommand MapToCommand(CreateTaskRequest createTaskRequest)
        => _mapper.Map<CreateTaskCommand>(createTaskRequest);

    public TaskResponse MapToResponse(TaskResult taskResult) => _mapper.Map<TaskResponse>(taskResult);
}
