namespace ToDoApp.Api.Middleware;

internal class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly bool _isDevelopmentEnvironment;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IWebHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _isDevelopmentEnvironment = environment.IsDevelopment();
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (NotFoundException exception)
        {
            var responseMessage = $"Resource with ID {exception.ResourceId} was not found";
            await WriteErrorResponseToContextAsync(httpContext, responseMessage, exception, 404);

            _logger.LogError(exception, "Resource with ID {ResourceId} was not found", exception.ResourceId);
        }
        catch (Exception exception)
        {
            await WriteErrorResponseToContextAsync(httpContext, "Unexpected error", exception, 500);

            _logger.LogError(exception, "Unexpected error");
        }
    }

    private async Task WriteErrorResponseToContextAsync(
        HttpContext httpContext,
        string errorResponseMessage,
        Exception exception,
        int statusCode)
    {
        httpContext.Response.StatusCode = statusCode;
        var errorResponse = _isDevelopmentEnvironment ? exception.ToString() : errorResponseMessage;
        await httpContext.Response.WriteAsJsonAsync(errorResponse);
    }
}
