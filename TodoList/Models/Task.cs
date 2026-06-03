namespace TodoList.Models;

internal class Task
{
    public static int LastId = 0;
    private int _id;
    protected readonly string Title;
    public bool IsDone = false;
    private DateTime _createdAt;

    public Task(string title)
    {
        _id = LastId;
        LastId++;
        Title = title;
        _createdAt = DateTime.Now;
    }
    
    public override string ToString()
    {
        return Title;
    }
}