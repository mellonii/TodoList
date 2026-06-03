namespace TodoList.Models;

internal class Task
{
    public readonly HashSet<string> Tags = [];
    public static int LastId = 0;
    public readonly string Title;
    
    private int _id;
    private DateTime _createdAt;
    
    public bool IsDone = false;

    public Task(string title)
    {
        _id = LastId;
        LastId++;
        Title = title;
        _createdAt = DateTime.Now;
    }
    
    public override string ToString()
    {
        if (Tags.Count == 0)
        {
            return Title;
        }
        else
        {
            return $"{Title} ({string.Join(", ", Tags)})";
        }
    }
}