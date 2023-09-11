namespace ToDoApp.Bll.Mapping;

internal interface IObjectMapper
{
    CreateTaskDto MapToDto(CreateTaskCommand createTaskCommand, DateTime creationDateUtc, string createdBy);

    TaskResult MapToResult(CreateTaskDto createTaskDto, Guid taskId);

    TaskResult MapToResult(TaskDto taskDto);
}
