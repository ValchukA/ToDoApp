﻿namespace ToDoApp.Api.Middleware;

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
            await WriteErrorResponseToContextAsync(httpContext, responseMessage, 404);

            _logger.LogError("Resource with ID {ResourceId} was not found", exception.ResourceId);
        }
        catch (Exception exception)
        {
            var responseMessage = _isDevelopmentEnvironment ? exception.ToString() : "Unexpected error";
            await WriteErrorResponseToContextAsync(httpContext, responseMessage, 500);

            _logger.LogError("Unexpected error", exception);
        }
    }

    private static async Task WriteErrorResponseToContextAsync(HttpContext httpContext, string errorResponseMessage, int statusCode)
    {
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(new ErrorResponse(errorResponseMessage));
    }
}
