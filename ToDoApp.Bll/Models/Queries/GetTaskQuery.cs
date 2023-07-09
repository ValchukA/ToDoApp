namespace ToDoApp.Bll.Models.Queries;

public record GetTaskQuery(Guid TaskId) : IRequest<TaskResult>;
