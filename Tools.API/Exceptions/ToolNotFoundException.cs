namespace Tools.API.Exceptions
{
    public class ToolNotFoundException(Guid Id) : NotFoundException("Tool", Id)
    {
    }
}
