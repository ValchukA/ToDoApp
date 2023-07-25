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
        var response = await _httpClient.PostAsJsonAsync("tasks", createTaskRequest);

        // Assert
        var taskResponse = await response.Content.ReadFromJsonAsync<TaskResponse>();

        response.StatusCode.Should().Be(HttpStatusCode.Created);
        taskResponse!.Id.Should().NotBeEmpty();
        taskResponse.Title.Should().Be(createTaskRequest.Title);
        taskResponse.Description.Should().Be(createTaskRequest.Description);
    }
}
