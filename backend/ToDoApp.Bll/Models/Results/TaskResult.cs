namespace ToDoApp.Bll.Models.Results;

public record TaskResult
{
    public required Guid Id { get; init; }

    public required string Title { get; init; }

    public string? Description { get; init; }
}
