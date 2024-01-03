using ToDoApp.Api.Auth;
using ToDoApp.Api.Features.Tasks.Mapping;

namespace ToDoApp.Api;

internal static class DependencyInjectionExtensions
{
    public static void AddApiServices(this IServiceCollection services, KeycloakOptions authOptions, IWebHostEnvironment environment)
    {
        services.AddControllers();
        services.AddSingleton<IObjectMapper, AutoMapperWrapper>();
        services.AddAuth(authOptions, environment);

        if (environment.IsDevelopment())
        {
            services.AddSwagger(authOptions);
        }
    }

    private static void AddAuth(this IServiceCollection services, KeycloakOptions authOptions, IWebHostEnvironment environment)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IUser, User>();
        services.AddAuthentication().AddJwtBearer(options =>
        {
            options.Authority = authOptions.Authority;
            options.Audience = authOptions.Audience;
            options.RequireHttpsMetadata = !environment.IsDevelopment() && !environment.IsEnvironment("Testing");
        });
    }

    private static void AddSwagger(this IServiceCollection services, KeycloakOptions authOptions)
    {
        const string oidcDefinitionName = "OIDC";

        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(oidcDefinitionName, new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OpenIdConnect,
                OpenIdConnectUrl = new Uri(authOptions.SwaggerOidcUrl),
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Id = oidcDefinitionName, Type = ReferenceType.SecurityScheme },
                    },
                    Array.Empty<string>()
                },
            });
        });
    }
}
