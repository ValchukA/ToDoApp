namespace ToDoApp.Common.Mapping;

public class AutoMapperWrapper : IObjectMapper<object>
{
    private readonly IMapper _mapper;

    public AutoMapperWrapper(IMapper mapper) => _mapper = mapper;

    public TDestination Map<TDestination>(object source) => _mapper.Map<TDestination>(source);
}
