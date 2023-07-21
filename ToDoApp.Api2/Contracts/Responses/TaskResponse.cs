namespace ToDoApp.Api.Contracts.Responses;

public record TaskResponse
{
    public required Guid Id { get; init; }

    public required string Title { get; init; }

    public string? Description { get; init; }
}
