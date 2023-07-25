namespace ToDoApp.Bll.Exceptions;

public class NotFoundException : Exception
{
    public required Guid ResourceId { get; init; }
}
