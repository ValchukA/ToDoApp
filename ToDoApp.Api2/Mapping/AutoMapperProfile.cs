namespace ToDoApp.Api.Mapping;

internal class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateTaskRequest, CreateTaskCommand>();
        CreateMap<TaskResult,  TaskResponse>();
    }
}
