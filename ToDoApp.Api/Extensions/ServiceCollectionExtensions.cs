namespace ToDoApp.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddControllers(options => options.Filters.Add<ValidationFilter>());
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddSwaggerGen();

        services.AddSingleton<IObjectMapper, AutoMapperWrapper>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(
            typeof(ServiceCollectionExtensions),
            ServiceLifetime.Singleton,
            includeInternalTypes: true);

        services.AddOptions<TaskValidationOptions>()
            .BindConfiguration(TaskValidationOptions.SectionKey)
            .ValidateFluentValidation()
            .ValidateOnStart();
    }
}
