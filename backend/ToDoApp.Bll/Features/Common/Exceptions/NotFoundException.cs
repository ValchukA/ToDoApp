namespace ToDoApp.Bll.Features.Common.Exceptions;

public class NotFoundException : Exception
{
    public required Guid ResourceId { get; init; }
}
