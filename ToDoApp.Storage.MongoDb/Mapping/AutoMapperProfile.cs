namespace ToDoApp.Storage.MongoDb.Mapping;

internal class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TaskEntity, TaskDto>();
        CreateMap<CreateTaskDto, TaskEntity>();
    }
}
