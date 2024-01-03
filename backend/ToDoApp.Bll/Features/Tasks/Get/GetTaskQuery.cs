namespace ToDoApp.Bll.Features.Tasks.Get;

public record GetTaskQuery(Guid Id) : IRequest<TaskModel>;
