namespace ToDoApp.Api.Extensions;

internal static class ServiceCollectionExtensions
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
            options.Filters.Add<ExceptionFilter>();
        });

        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        services.AddSwaggerGen();

        services.AddAutoMapper(typeof(ServiceCollectionExtensions));
        services.AddSingleton<IObjectMapper<TasksController>, AutoMapperWrapper>();

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceCollectionExtensions), includeInternalTypes: true);
    }
}
