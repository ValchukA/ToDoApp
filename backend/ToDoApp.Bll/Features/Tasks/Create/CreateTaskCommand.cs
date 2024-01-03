namespace ToDoApp.Bll.Features.Tasks.Create;

public record CreateTaskCommand : IRequest<TaskModel>
{
    public required string Title { get; init; }

    public string? Description { get; init; }
}
