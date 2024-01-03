namespace ToDoApp.Storage.MongoDb;

public static class DependencyInjectionExtensions
{
    public static void AddMongoStorageServices(this IServiceCollection services, IConfigurationSection configuration)
    {
        var mongoDbOptions = configuration.GetRequiredSection(MongoDbOptions.SectionKey).Get<MongoDbOptions>()!;
        new MongoDbOptionsValidator().ValidateAndThrow(mongoDbOptions);
        var mongoDbSettings = MongoClientSettings.FromConnectionString(mongoDbOptions.ConnectionString);

        services.AddSingleton(serviceProvider =>
        {
            mongoDbSettings.LoggingSettings = new LoggingSettings(serviceProvider.GetRequiredService<ILoggerFactory>());

            return new MongoClient(mongoDbSettings).GetDatabase(mongoDbOptions.DatabaseName);
        });
        services.AddSingleton<ITaskRepository, TaskRepository>();
        services.AddSingleton<IObjectMapper, MapperlyMapper>();
    }
}
