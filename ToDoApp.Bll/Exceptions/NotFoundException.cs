namespace ToDoApp.Bll.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException()
        : base("Resource was not found.")
    {
    }
}
