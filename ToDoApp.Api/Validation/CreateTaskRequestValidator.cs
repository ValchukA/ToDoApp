namespace ToDoApp.Api.Validation;

internal class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(model => model.Title).NotEmpty().MaximumLength(100);
        RuleFor(model => model.Description).MaximumLength(2000);
    }
}
