namespace ToDoApp.Bll.Features.Common.Exceptions;

public class UnauthorizedException : Exception
{
    public required Guid ResourceId { get; init; }
}
