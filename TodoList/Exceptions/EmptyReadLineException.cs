namespace TodoList.Exceptions;

public class EmptyReadLineException : Exception
{
    public EmptyReadLineException() : base("Была введена пустая строка") {}
    
}