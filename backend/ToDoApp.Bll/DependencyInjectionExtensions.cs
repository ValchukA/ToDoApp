namespace ToDoApp.Bll;

public static class DependencyInjectionExtensions
{
    public static void AddBllServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining(typeof(DependencyInjectionExtensions));
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssemblyContaining(
            typeof(DependencyInjectionExtensions),
            ServiceLifetime.Singleton,
            includeInternalTypes: true);

        services.AddSingleton<IObjectMapper, ObjectMapper>();
    }
}
