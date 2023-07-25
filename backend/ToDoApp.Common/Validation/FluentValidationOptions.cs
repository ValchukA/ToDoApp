namespace ToDoApp.Common.Validation;

internal class FluentValidationOptions<TOptions> : IValidateOptions<TOptions>
    where TOptions : class
{
    private readonly string? _name;
    private readonly IValidator<TOptions> _validator;

    public FluentValidationOptions(string? name, IValidator<TOptions> validator)
    {
        _name = name;
        _validator = validator;
    }

    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (_name is not null && _name != name)
        {
            return ValidateOptionsResult.Skip;
        }

        ArgumentNullException.ThrowIfNull(options);

        var validationResult = _validator.Validate(options);

        if (validationResult.IsValid)
        {
            return ValidateOptionsResult.Success;
        }

        var errors = validationResult.Errors.Select(error => error.ErrorMessage);

        return ValidateOptionsResult.Fail(errors);
    }
}
