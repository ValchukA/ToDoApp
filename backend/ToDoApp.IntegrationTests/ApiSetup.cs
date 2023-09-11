namespace ToDoApp.IntegrationTests;

public class ApiSetup : IAsyncLifetime
{
    private const int _apiContainerHttpPort = 80;
    private const string _mongoDbNetworkAlias = "mongodb";
    private const string _keycloakNetworkAlias = "keycloak";

    private readonly IContainer _mongoDbContainer;
    private readonly IContainer _keycloakContainer;
    private readonly IFutureDockerImage _apiImage;
    private readonly IContainer _apiContainer;

    public ApiSetup()
    {
        var network = new NetworkBuilder().Build();
        var projectDirectory = CommonDirectoryPath.GetProjectDirectory().DirectoryPath;
        var solutionDirectory = CommonDirectoryPath.GetSolutionDirectory().DirectoryPath;

        _mongoDbContainer = BuildMongoDbContainer(network);
        _keycloakContainer = BuildKeycloakContainer(network, solutionDirectory, projectDirectory);
        _apiImage = BuildApiImage(solutionDirectory);
        _apiContainer = BuildApiContainer(network);
    }

    public HttpClient HttpClient { get; } = new();

    public async Task InitializeAsync()
    {
        await Task.WhenAll(_mongoDbContainer.StartAsync(), _keycloakContainer.StartAsync(), _apiImage.CreateAsync());
        await _apiContainer.StartAsync();

        HttpClient.BaseAddress = new UriBuilder(
            "http",
            _apiContainer.Hostname,
            _apiContainer.GetMappedPublicPort(_apiContainerHttpPort),
            "api/").Uri;
    }

    public Task DisposeAsync()
    {
        HttpClient.Dispose();

        return Task.CompletedTask;
    }

    private static MongoDbContainer BuildMongoDbContainer(INetwork network)
        => new MongoDbBuilder()
            .WithImage("mongo:7.0.1")
            .WithNetwork(network)
            .WithNetworkAliases(_mongoDbNetworkAlias)
            .Build();

    private static KeycloakContainer BuildKeycloakContainer(INetwork network, string solutionDirectory, string projectDirectory)
    {
        const string keycloakImportPath = "/opt/keycloak/data/import";

        var realmHostFile = new FileInfo($"{solutionDirectory}/keycloak-todoapp-realm.json");
        var realmUsersHostFile = new FileInfo($"{projectDirectory}/keycloak-todoapp-realm-users.json");

        var realmDockerFile = new FileInfo($"{keycloakImportPath}/todoapp-realm.json");
        var realmUsersDockerFile = new FileInfo($"{keycloakImportPath}/todoapp-users-0.json");

        return new KeycloakBuilder()
            .WithImage("quay.io/keycloak/keycloak:22.0.1")
            .WithCommand("--import-realm")
            .WithResourceMapping(realmHostFile, realmDockerFile)
            .WithResourceMapping(realmUsersHostFile, realmUsersDockerFile)
            .WithNetwork(network)
            .WithNetworkAliases(_keycloakNetworkAlias)
            .Build();
    }

    private static IFutureDockerImage BuildApiImage(string solutionDirectory)
        => new ImageFromDockerfileBuilder()
            .WithDockerfileDirectory(solutionDirectory)
            .WithDockerfile("backend/ToDoApp.Api/Dockerfile")
            .Build();

    private IContainer BuildApiContainer(INetwork network)
    {
        var mongoDbConnectionString = $"mongodb://{_mongoDbNetworkAlias}:{MongoDbBuilder.MongoDbPort}";
        var keycloakUrl = $"http://{_keycloakNetworkAlias}:{KeycloakBuilder.KeycloakPort}/realms/todoapp";

        return new ContainerBuilder()
            .WithImage(_apiImage)
            .WithEnvironment("MongoDb__ConnectionString", mongoDbConnectionString)
            .WithEnvironment("Keycloak__Authority", keycloakUrl)
            .WithPortBinding(_apiContainerHttpPort, true)
            .WithNetwork(network)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(_apiContainerHttpPort))
            .Build();
    }
}
