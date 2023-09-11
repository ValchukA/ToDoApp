namespace ToDoApp.Bll.Exceptions;

public class UnauthorizedException : Exception
{
    public required Guid ResourceId { get; init; }
}
