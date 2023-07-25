namespace ToDoApp.Api.Options;

internal record TaskValidationOptions
{
    public const string SectionKey = "TaskValidation";

    public required int MaximumTitleLength { get; init; }

    public required int MaximumDescriptionLength { get; init; }
}
