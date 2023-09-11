namespace ToDoApp.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static void AddApiServices(this IServiceCollection services, KeycloakOptions authOptions, IWebHostEnvironment environment)
    {
        services.AddControllers(options => options.Filters.Add<ValidationFilter>());
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        services.AddSingleton<IObjectMapper, AutoMapperWrapper>();
        services.AddValidation();
        services.AddOptions();
        services.AddAuth(authOptions, environment);

        if (environment.IsDevelopment())
        {
            services.AddSwagger(authOptions);
        }
    }

    private static void AddValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(
            typeof(ServiceCollectionExtensions),
            ServiceLifetime.Singleton,
            filter => filter.ValidatorType != typeof(KeycloakOptionsValidator),
            true);
    }

    private static void AddOptions(this IServiceCollection services)
        => services.AddOptions<TaskValidationOptions>()
            .BindConfiguration(TaskValidationOptions.SectionKey)
            .ValidateFluentValidation()
            .ValidateOnStart();

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
