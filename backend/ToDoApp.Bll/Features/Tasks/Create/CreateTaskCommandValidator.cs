namespace ToDoApp.Bll.Features.Tasks.Create;

internal class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskCommandValidator()
    {
        RuleFor(model => model.Title).NotEmpty().MaximumLength(100);
        RuleFor(model => model.Description).MaximumLength(2000);
    }
}
