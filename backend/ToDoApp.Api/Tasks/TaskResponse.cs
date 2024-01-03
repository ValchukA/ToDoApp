namespace ToDoApp.Api.Tasks;

public record TaskResponse
{
    public required Guid Id { get; init; }

    public required string Title { get; init; }

    public string? Description { get; init; }
}
