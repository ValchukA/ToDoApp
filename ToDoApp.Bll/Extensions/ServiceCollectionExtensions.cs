namespace ToDoApp.Bll.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddBllServices(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(ServiceCollectionExtensions)));

        services.AddAutoMapper(typeof(ServiceCollectionExtensions));
        services.AddSingleton<IObjectMapper<GetTaskHandler>, AutoMapperWrapper>();
        services.AddSingleton<IObjectMapper<CreateTaskHandler>, AutoMapperWrapper>();
    }
}
