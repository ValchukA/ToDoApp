namespace ToDoApp.Bll.Models.Commands;

public record CreateTaskCommand : IRequest<TaskResult>
{
    public required string Title { get; init; }

    public string? Description { get; init; }
}
