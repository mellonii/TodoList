namespace TodoList.Exceptions;

public class EmptyReadLineException : BusinesException
{
    public EmptyReadLineException() : base("Была введена пустая строка") {}
    
}