namespace TodoList.Exceptions;

public class InvalidTodoDataException : Exception
{
    public InvalidTodoDataException(string message) : base(message) {}
}