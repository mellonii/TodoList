namespace TodoList.Exceptions;

public class BusinesException(string message) : Exception(message)
{
    public override string Message { get; } = message;
}