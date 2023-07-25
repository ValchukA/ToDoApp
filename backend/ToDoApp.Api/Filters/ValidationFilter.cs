namespace ToDoApp.Api.Filters;

internal class ValidationFilter : IAsyncActionFilter
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ValidationFilter(ILogger<ExceptionMiddleware> logger) => _logger = logger;

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errorMessages = context.ModelState
                .SelectMany(entry => entry.Value?.Errors ?? Enumerable.Empty<ModelError>())
                .Select(error => error.ErrorMessage);

            var errorResponse = new ErrorResponse(string.Join(" ", errorMessages));
            context.Result = new BadRequestObjectResult(errorResponse);

            _logger.LogError("Model validation failed");

            return;
        }

        await next();
    }
}
