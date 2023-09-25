namespace ProtoCourse.Core.Exceptions;

public class ForbiddenException : ApplicationException
{
    public ForbiddenException() : base($"Forbidden")
    {

    }
}
