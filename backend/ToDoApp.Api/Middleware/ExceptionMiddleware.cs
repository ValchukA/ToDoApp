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
        catch (UnauthorizedException exception)
        {
            await WriteErrorResponseAsync(
                httpContext,
                403,
                $"Unauthorized access to resource with ID {exception.ResourceId}",
                exception);

            _logger.LogError(exception, "Unauthorized access to resource with ID {ResourceId}", exception.ResourceId);
        }
        catch (NotFoundException exception)
        {
            await WriteErrorResponseAsync(
                httpContext,
                404,
                $"Resource with ID {exception.ResourceId} was not found",
                exception);

            _logger.LogError(exception, "Resource with ID {ResourceId} was not found", exception.ResourceId);
        }
        catch (Exception exception)
        {
            await WriteErrorResponseAsync(
                httpContext,
                500,
                "Unexpected error",
                exception);

            _logger.LogError(exception, "Unexpected error");
        }
    }

    private async Task WriteErrorResponseAsync(
        HttpContext httpContext,
        int statusCode,
        string errorMessage,
        Exception exception)
    {
        var errorResponse = new ErrorResponse
        {
            ErrorMessage = _isDevelopmentEnvironment ? exception.ToString() : errorMessage,
            RequestId = httpContext.TraceIdentifier,
        };

        await httpContext.Response.WriteAsJsonAsync(errorResponse);
        httpContext.Response.StatusCode = statusCode;
    }
}
