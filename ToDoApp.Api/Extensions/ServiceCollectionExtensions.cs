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

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAutoMapper(typeof(ServiceCollectionExtensions));
        services.AddSingleton<IObjectMapper<TasksController>, AutoMapperWrapper>();

        services.AddFluentValidationAutoValidation();
        services.AddSingleton<IValidator<CreateTaskRequest>, CreateTaskRequestValidator>();
        ValidatorOptions.Global.LanguageManager.Enabled = false;
    }
}
