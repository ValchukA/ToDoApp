namespace ToDoApp.Api.Features.Tasks;

public record CreateTaskRequest
{
    public required string Title { get; init; }

    public string? Description { get; init; }
}
