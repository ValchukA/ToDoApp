namespace ToDoApp.Storage.MongoDb.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMongoStorageServices(this IServiceCollection services)
    {
        services.AddSingleton<IValidator<MongoDbOptions>, MongoDbOptionsValidator>();
        services.AddMongoOptions();

        services.AddMongoDatabase();
        services.AddSingleton<ITaskDao, TaskDao>();

        services.AddAutoMapper(typeof(ServiceCollectionExtensions));
        services.AddSingleton<IObjectMapper<TaskDao>, AutoMapperWrapper>();
    }

    private static void AddMongoOptions(this IServiceCollection services)
        => services.AddOptions<MongoDbOptions>()
            .BindConfiguration(MongoDbOptions.SectionKey)
            .ValidateFluentValidation()
            .ValidateOnStart();

    private static void AddMongoDatabase(this IServiceCollection services)
    {
        services.TryAddSingleton(serviceProvider =>
        {
            var mongoDbOptions = serviceProvider.GetRequiredService<IOptions<MongoDbOptions>>().Value;
            var settings = MongoClientSettings.FromConnectionString(mongoDbOptions.ConnectionString);
            settings.LinqProvider = LinqProvider.V3;
            settings.LoggingSettings = new LoggingSettings(serviceProvider.GetRequiredService<ILoggerFactory>());

            return new MongoClient(settings).GetDatabase(mongoDbOptions.DatabaseName);
        });
    }
}
