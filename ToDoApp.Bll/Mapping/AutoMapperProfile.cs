namespace ToDoApp.Bll.Mapping;

internal class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateTaskCommand, CreateTaskDto>();
        CreateMap<CreateTaskDto, TaskResult>();
        CreateMap<TaskDto, TaskResult>();
    }
}
