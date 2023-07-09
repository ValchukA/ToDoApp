namespace ToDoApp.Bll.Storage.Dtos;

public record CreateTaskDto
{
    public required string Title { get; init; }

    public string? Description { get; init; }

    public required DateTime CreationDateUtc { get; init; }
}
