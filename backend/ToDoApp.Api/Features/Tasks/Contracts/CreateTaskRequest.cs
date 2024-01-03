namespace ToDoApp.Api.Features.Tasks.Contracts;

public record CreateTaskRequest
{
    public required string Title { get; init; }

    public string? Description { get; init; }
}
