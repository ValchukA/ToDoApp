namespace ToDoApp.Api.Auth;

internal record KeycloakOptions
{
    public const string SectionKey = "Keycloak";

    public required string Authority { get; init; }

    public required string Audience { get; init; }

    public required string SwaggerClientId { get; init; }

    public required string SwaggerOidcUrl { get; init; }
}
