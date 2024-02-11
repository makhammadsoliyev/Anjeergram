namespace Anjeergram.Exceptions;

public class NotFoundException<T> : Exception
{
    public int StatusCode { get; }
    public override string Message { get; }

    public NotFoundException()
    {
        Message = $"{typeof(T).Name} was not found";
        StatusCode = 404;
    }
}