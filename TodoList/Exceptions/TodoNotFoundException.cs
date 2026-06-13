namespace TodoList.Exceptions;

public class TodoNotFoundException : Exception
{
    public TodoNotFoundException() : base("Задачи с таким id нет") {}
}