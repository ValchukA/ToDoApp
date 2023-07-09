namespace ToDoApp.Api.Filters;

internal class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errorMessages = context.ModelState
                .SelectMany(entry => entry.Value?.Errors ?? Enumerable.Empty<ModelError>())
                .Select(error => error.ErrorMessage);

            var errorResponse = new ErrorResponse(string.Join(" ", errorMessages));
            context.Result = new BadRequestObjectResult(errorResponse);

            return;
        }

        await next();
    }
}
