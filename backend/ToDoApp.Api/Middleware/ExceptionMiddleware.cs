namespace ToDoApp.Api.Middleware;

internal class ExceptionMiddleware
{
    private const string _unauthorizedErrorTemplate = "Unauthorized access to resource with ID {0}";
    private const string _notFoundErrorTemplate = "Resource with ID {0} was not found";
    private const string _unexpectedErrorTemplate = "Unexpected error";

    private readonly string _unauthorizedErrorLoggingTemplate = string.Format(_unauthorizedErrorTemplate, "{ResourceId}");
    private readonly string _notFoundErrorLoggingTemplate = string.Format(_notFoundErrorTemplate, "{ResourceId}");

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
            await WriteErrorResponseToContextAsync(
                httpContext,
                403,
                exception,
                _unauthorizedErrorTemplate,
                _unauthorizedErrorLoggingTemplate,
                exception.ResourceId);
        }
        catch (NotFoundException exception)
        {
            await WriteErrorResponseToContextAsync(
                httpContext,
                404,
                exception,
                _notFoundErrorTemplate,
                _notFoundErrorLoggingTemplate,
                exception.ResourceId);
        }
        catch (Exception exception)
        {
            await WriteErrorResponseToContextAsync(
                httpContext,
                500,
                exception,
                _unexpectedErrorTemplate,
                _unexpectedErrorTemplate);
        }
    }

    private async Task WriteErrorResponseToContextAsync(
        HttpContext httpContext,
        int statusCode,
        Exception exception,
        string responseTemplate,
        string loggingTemplate,
        params object[] templateParameters)
    {
        httpContext.Response.StatusCode = statusCode;
        var selectedResponse = _isDevelopmentEnvironment ? exception.ToString() : string.Format(responseTemplate, templateParameters);
        await httpContext.Response.WriteAsJsonAsync(new ErrorResponse(selectedResponse, httpContext.TraceIdentifier));

        _logger.LogError(exception, loggingTemplate, templateParameters);
    }
}
