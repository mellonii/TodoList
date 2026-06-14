namespace TodoList.Models;

public class Todo
{
    public readonly HashSet<string> Tags = [];
    public static int LastId;
    public static int CurrentTasksCount;
    public static int DoneTasksCount = 0;
    
    private int _id;
    private DateTimeOffset _createdAt;
    
    public readonly string Title;
    public bool IsDone = false;
    public int Priority = 0;

    public Todo(string title)
    {
        _id = LastId;
        LastId++;
        CurrentTasksCount++;
        Title = title;
        _createdAt = DateTimeOffset.Now;
    }
    
    public override string ToString()
    {
        if (Tags.Count == 0) return Priority == 0 ? Title : $"{Title} - ☆ {Priority} ☆ ";
        if (Priority == 0) return $"{Title} ({string.Join(", ", Tags)}) ";
        return $"{Title} ({string.Join(", ", Tags)}) - ☆ {Priority} ☆ ";
    }
}