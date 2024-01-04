namespace ToDoApp.Bll.Features.Common.Exceptions;

public class ValidationException : Exception
{
    public required IEnumerable<string> Errors { get; init; }
}
