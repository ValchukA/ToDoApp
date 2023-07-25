namespace ToDoApp.Api.Validation;

internal class TaskValidationOptionsValidator : AbstractValidator<TaskValidationOptions>
{
    public TaskValidationOptionsValidator()
    {
        RuleFor(options => options.MaximumTitleLength).GreaterThan(0);
        RuleFor(options => options.MaximumDescriptionLength).GreaterThan(0);
    }
}
