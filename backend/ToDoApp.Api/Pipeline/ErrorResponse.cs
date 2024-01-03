namespace ToDoApp.Api.Pipeline;

internal record ErrorResponse
{
    public required string ErrorMessage { get; init; }

    public required string RequestId { get; init; }
}
