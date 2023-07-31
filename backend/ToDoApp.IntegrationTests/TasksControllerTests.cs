namespace ToDoApp.IntegrationTests;

public class TasksControllerTests : IClassFixture<ApiSetup>
{
    private readonly HttpClient _httpClient;

    public TasksControllerTests(ApiSetup apiSetup) => _httpClient = apiSetup.HttpClient;

    [Fact]
    public async Task CreateTaskAsync_ReturnsCreatedTask_WhenEverythingIsOk()
    {
        // Arrange
        var createTaskRequest = new CreateTaskRequest { Title = "First task", Description = "Description of the first task" };

        // Act
        var createTaskHttpResponse = await _httpClient.PostAsJsonAsync("tasks", createTaskRequest);

        // Assert
        var createTaskResponse = await createTaskHttpResponse.Content.ReadFromJsonAsync<TaskResponse>();

        createTaskHttpResponse.StatusCode.Should().Be(HttpStatusCode.Created);
        createTaskResponse!.Id.Should().NotBeEmpty();
        createTaskResponse.Title.Should().Be(createTaskRequest.Title);
        createTaskResponse.Description.Should().Be(createTaskRequest.Description);
    }

    [Fact]
    public async Task CreateTaskAsync_ReturnsValidationError_WhenModelIsInvalid()
    {
        // Arrange
        var createTaskRequest = new CreateTaskRequest { Title = string.Empty, Description = "Description of the task" };

        // Act
        var createTaskHttpResponse = await _httpClient.PostAsJsonAsync("tasks", createTaskRequest);

        // Assert
        var createTaskResponse = await createTaskHttpResponse.Content.ReadFromJsonAsync<ErrorResponse>();

        createTaskHttpResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        createTaskResponse!.ErrorMessage.Should().Be("'Title' must not be empty.");
        createTaskResponse.RequestId.Should().BeNull();
    }

    [Fact]
    public async Task GetTaskAsync_ReturnsTask_WhenEverythingIsOk()
    {
        // Arrange
        var createTaskRequest = new CreateTaskRequest { Title = "Second task", Description = "Description of the second task" };
        var createTaskHttpResponse = await _httpClient.PostAsJsonAsync("tasks", createTaskRequest);
        var taskId = (await createTaskHttpResponse.Content.ReadFromJsonAsync<TaskResponse>())!.Id;

        // Act
        var getTaskHttpResponse = await _httpClient.GetAsync($"tasks/{taskId}");

        // Assert
        var getTaskResponse = await getTaskHttpResponse.Content.ReadFromJsonAsync<TaskResponse>();

        getTaskHttpResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        getTaskResponse!.Id.Should().Be(taskId);
        getTaskResponse.Title.Should().Be(createTaskRequest.Title);
        getTaskResponse.Description.Should().Be(createTaskRequest.Description);
    }

    [Fact]
    public async Task GetTaskAsync_ReturnsNotFoundError_WhenTaskIsNotFoundById()
    {
        // Act
        var getTaskHttpResponse = await _httpClient.GetAsync($"tasks/{Guid.Empty}");

        // Assert
        var getTaskResponse = await getTaskHttpResponse.Content.ReadFromJsonAsync<ErrorResponse>();

        getTaskHttpResponse.StatusCode.Should().Be(HttpStatusCode.NotFound);
        getTaskResponse!.ErrorMessage.Should().Be($"Resource with ID {Guid.Empty} was not found");
        getTaskResponse.RequestId.Should().NotBeEmpty();
    }
}
