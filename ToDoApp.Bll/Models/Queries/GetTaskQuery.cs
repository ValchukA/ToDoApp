namespace ToDoApp.Bll.Models.Queries;

public record GetTaskQuery(Guid Id) : IRequest<TaskResult>;
