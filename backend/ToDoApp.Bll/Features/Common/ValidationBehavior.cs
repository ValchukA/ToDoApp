namespace ToDoApp.Bll.Features.Common;

internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehavior(IValidator<TRequest> validator) => _validator = validator;

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(request);

        return !validationResult.IsValid
            ? throw new ValidationException { Errors = validationResult.Errors.Select(error => error.ErrorMessage) }
            : next();
    }
}
