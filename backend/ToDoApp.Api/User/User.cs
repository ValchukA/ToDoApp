namespace ToDoApp.Api.User;

internal class User : IUser
{
    public User(IHttpContextAccessor httpContextAccessor, ILogger<User> logger)
    {
        Username = httpContextAccessor.HttpContext?.User.FindFirst("preferred_username")?.Value!;

        logger.LogInformation("Username of authenticated user: {Username}", Username);
    }

    public string Username { get; }
}
