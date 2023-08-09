namespace ToDoApp.Api.Tests;

public class CreateTaskRequestValidatorTests
{
    private readonly CreateTaskRequestValidator _validator;

    public CreateTaskRequestValidatorTests()
    {
        var validationOptions = Substitute.For<IOptions<TaskValidationOptions>>();
        validationOptions.Value.Returns(new TaskValidationOptions { MaximumTitleLength = 15, MaximumDescriptionLength = 30 });
        _validator = new CreateTaskRequestValidator(validationOptions);
    }

    [Theory]
    [MemberData(nameof(ValidRequestModels))]
    public void Validate_ShouldNotHaveErrors_WhenRequestModelIsValid(CreateTaskRequest requestModel)
    {
        // Act
        var validationResult = _validator.TestValidate(requestModel);

        // Assert
        validationResult.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [MemberData(nameof(InvalidRequestModels))]
    public void Validate_ShouldHaveErrors_WhenRequestModelIsInvalid(CreateTaskRequest requestModel)
    {
        // Act
        var validationResult = _validator.TestValidate(requestModel);

        // Assert
        validationResult.ShouldHaveAnyValidationError();
    }

    public static IEnumerable<object[]> ValidRequestModels => new object[][]
    {
        new object[] { new CreateTaskRequest { Title = "First task" } },
        new object[] { new CreateTaskRequest { Title = "Second task", Description = "Description of the second task" } },
    };

    public static IEnumerable<object[]> InvalidRequestModels => new object[][]
    {
        new object[] { new CreateTaskRequest { Title = null! } },
        new object[] { new CreateTaskRequest { Title = string.Empty } },
        new object[] { new CreateTaskRequest { Title = string.Empty, Description = "Description" } },
        new object[] { new CreateTaskRequest { Title = null!, Description = "Description" } },
        new object[] { new CreateTaskRequest { Title = "Fifth task", Description = "Long description of the fifth task" } },
        new object[] { new CreateTaskRequest { Title = "Long title of the sixth task", Description = "Description" } },
    };
}
