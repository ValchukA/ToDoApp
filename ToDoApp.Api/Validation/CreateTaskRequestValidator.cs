namespace ToDoApp.Api.Validation;

internal class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator(IOptions<TaskValidationOptions> validationOptions)
    {
        RuleFor(model => model.Title).NotEmpty().MaximumLength(validationOptions.Value.MaximumTitleLength);
        RuleFor(model => model.Description).MaximumLength(validationOptions.Value.MaximumDescriptionLength);
    }
}
