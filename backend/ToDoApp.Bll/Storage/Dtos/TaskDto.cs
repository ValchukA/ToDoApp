namespace ToDoApp.Bll.Storage.Dtos;

public record TaskDto
{
    public Guid Id { get; init; }

    public required string Title { get; init; }

    public string? Description { get; init; }

    public required string CreatedBy { get; init; }
}
