namespace ToDoApp.Api.Filters;

internal class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var statusCode = context.Exception switch
        {
            NotFoundException => 404,
            _ => 500,
        };

        var errorResponse = new ErrorResponse(context.Exception.Message);
        context.Result = new ObjectResult(errorResponse) { StatusCode = statusCode };
    }
}
