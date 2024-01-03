namespace ToDoApp.Api.Tasks;

public record CreateTaskRequest
{
    public required string Title { get; init; }

    public string? Description { get; init; }
}
