namespace ToDoApp.IntegrationTests;

public class ApiSetup : IAsyncLifetime
{
    private const string _mongoDbNetworkAlias = "mongodb";
    private const int _apiContainerHttpPort = 80;

    private readonly IContainer _mongoDbContainer;
    private readonly IFutureDockerImage _apiImage;
    private readonly IContainer _apiContainer;

    public ApiSetup()
    {
        var network = new NetworkBuilder().Build();

        _mongoDbContainer = new MongoDbBuilder()
            .WithNetwork(network)
            .WithNetworkAliases(_mongoDbNetworkAlias)
            .Build();

        _apiImage = new ImageFromDockerfileBuilder()
            .WithDockerfileDirectory(CommonDirectoryPath.GetProjectDirectory(), "..")
            .WithDockerfile("ToDoApp.Api/Dockerfile")
            .Build();

        _apiContainer = new ContainerBuilder()
            .WithImage(_apiImage)
            .WithEnvironment("MongoDb__ConnectionString", $"mongodb://mongo:mongo@{_mongoDbNetworkAlias}:27017")
            .WithPortBinding(_apiContainerHttpPort, true)
            .WithNetwork(network)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(_apiContainerHttpPort))
            .Build();

        HttpClient = new HttpClient();
    }

    public HttpClient HttpClient { get; }

    public Task DisposeAsync()
    {
        HttpClient.Dispose();

        return Task.CompletedTask;
    }

    public async Task InitializeAsync()
    {
        await Task.WhenAll(_mongoDbContainer.StartAsync(), _apiImage.CreateAsync());
        await _apiContainer.StartAsync();

        HttpClient.BaseAddress = new UriBuilder(
            "http",
            _apiContainer.Hostname,
            _apiContainer.GetMappedPublicPort(_apiContainerHttpPort),
            "api/").Uri;
    }
}
