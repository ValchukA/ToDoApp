namespace ToDoApp.Api.Contracts.Requests;

public record CreateTaskRequest
{
    public required string Title { get; init; }

    public string? Description { get; init; }
}
