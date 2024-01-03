namespace ToDoApp.Api.Pipeline;

internal class RequestIdLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestIdLoggingMiddleware> _logger;

    public RequestIdLoggingMiddleware(RequestDelegate next, ILogger<RequestIdLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        using (_logger.BeginScope(new Dictionary<string, string> { { "ToDoApiRequestId", httpContext.TraceIdentifier } }))
        {
            await _next(httpContext);
        }
    }
}
