namespace TodoList.Exceptions;

public class TodoNotFoundException : BusinesException
{
    public TodoNotFoundException() : base("Задачи с таким id нет") {}
}