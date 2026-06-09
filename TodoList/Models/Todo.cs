namespace TodoList.Models;

internal class Todo
{
    public readonly HashSet<string> Tags = [];
    public static int LastId;
    public static int CurrentTasksCount;
    public static int DoneTasksCount = 0;
    public readonly string Title;
    
    private int _id;
    private DateTimeOffset _createdAt;
    
    public bool IsDone = false;

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
        if (Tags.Count == 0)
        {
            return Title;
        }
        return $"{Title} ({string.Join(", ", Tags)})";
    }
}