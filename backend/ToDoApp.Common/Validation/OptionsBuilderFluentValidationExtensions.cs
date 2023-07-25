namespace ToDoApp.Common.Validation;

public static class OptionsBuilderFluentValidationExtensions
{
    public static OptionsBuilder<TOptions> ValidateFluentValidation<TOptions>(this OptionsBuilder<TOptions> optionsBuilder)
        where TOptions : class
    {
        optionsBuilder.Services.AddSingleton<IValidateOptions<TOptions>>(provider =>
        {
            var validator = provider.GetRequiredService<IValidator<TOptions>>();

            return new FluentValidationOptions<TOptions>(optionsBuilder.Name, validator);
        });

        return optionsBuilder;
    }
}
