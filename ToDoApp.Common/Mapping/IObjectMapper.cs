namespace ToDoApp.Common.Mapping;

public interface IObjectMapper<in THolder>
{
    TDestination Map<TDestination>(object source);
}
