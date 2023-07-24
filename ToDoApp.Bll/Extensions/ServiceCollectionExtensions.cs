namespace ToDoApp.Bll.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddBllServices(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions)));

        services.AddSingleton<IObjectMapper, ObjectMapper>();
    }
}
