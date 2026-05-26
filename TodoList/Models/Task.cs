namespace TodoList.Models;

internal class Task
{
    private static int _lastId = 0;
    private int _id;
    protected readonly string Title;
    public bool IsDone = false;
    private DateTime _createdAt;

    public Task(string title)
    {
        _id = _lastId;
        _lastId++;
        Title = title;
        _createdAt = DateTime.Now;
    }
    
    public override string ToString()
    {
        return Title;
    }
}