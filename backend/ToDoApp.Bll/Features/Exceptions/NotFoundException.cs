namespace ToDoApp.Bll.Features.Exceptions;

public class NotFoundException : Exception
{
    public required Guid ResourceId { get; init; }
}
