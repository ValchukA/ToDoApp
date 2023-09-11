namespace ToDoApp.Bll.Mapping;

internal class ObjectMapper : IObjectMapper
{
    public CreateTaskDto MapToDto(CreateTaskCommand createTaskCommand, DateTime creationDateUtc, string createdBy) => new()
    {
        Title = createTaskCommand.Title,
        Description = createTaskCommand.Description,
        CreationDateUtc = creationDateUtc,
        CreatedBy = createdBy,
    };

    public TaskResult MapToResult(CreateTaskDto createTaskDto, Guid taskId) => new()
    {
        Id = taskId,
        Title = createTaskDto.Title,
        Description = createTaskDto.Description,
    };

    public TaskResult MapToResult(TaskDto taskDto) => new()
    {
        Id = taskDto.Id,
        Title = taskDto.Title,
        Description = taskDto.Description,
    };
}
