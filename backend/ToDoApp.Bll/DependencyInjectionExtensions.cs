using ToDoApp.Bll.Features.Common.Mapping;

namespace ToDoApp.Bll;

public static class DependencyInjectionExtensions
{
    public static void AddBllServices(this IServiceCollection services)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining(typeof(DependencyInjectionExtensions)));
        services.AddSingleton<ITaskMapper, TaskMapper>();
    }
}
