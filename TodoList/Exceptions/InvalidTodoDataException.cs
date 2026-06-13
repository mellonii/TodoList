namespace TodoList.Exceptions;

public class InvalidTodoDataException : Exception
{
    public InvalidTodoDataException() : base("Неправильно введен id задачи") {}
}