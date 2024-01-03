namespace ToDoApp.Api.Options;

internal class KeycloakOptionsValidator : AbstractValidator<KeycloakOptions>
{
    public KeycloakOptionsValidator(bool isDevelopment)
    {
        RuleFor(options => options.Authority).NotEmpty();
        RuleFor(options => options.Audience).NotEmpty();

        if (isDevelopment)
        {
            RuleFor(options => options.SwaggerClientId).NotEmpty();
            RuleFor(options => options.SwaggerOidcUrl).NotEmpty();
        }
    }
}
