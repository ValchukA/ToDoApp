namespace ToDoApp.Storage.MongoDb.Validation;

internal class MongoDbOptionsValidator : AbstractValidator<MongoDbOptions>
{
    public MongoDbOptionsValidator()
    {
        RuleFor(options => options.ConnectionString).NotEmpty();
        RuleFor(options => options.DatabaseName).NotEmpty();
    }
}
