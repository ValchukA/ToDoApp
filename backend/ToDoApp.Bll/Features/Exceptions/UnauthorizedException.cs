namespace ToDoApp.Bll.Features.Exceptions;

public class UnauthorizedException : Exception
{
    public required Guid ResourceId { get; init; }
}
