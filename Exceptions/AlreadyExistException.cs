namespace Anjeergram.Exceptions;

public class AlreadyExistException<T> : Exception
{
    public int StatusCode { get; }
    public override string Message { get; }

    public AlreadyExistException()
    {
        Message = $"{typeof(T).Name} already exists";
        StatusCode = 400;
    }
}