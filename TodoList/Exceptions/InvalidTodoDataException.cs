namespace TodoList.Exceptions;

public class InvalidTodoDataException : BusinesException
{
    public InvalidTodoDataException() : base("Неправильно введен id задачи") {}
}