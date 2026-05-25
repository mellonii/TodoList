namespace TodoList;

internal class Task
{
    private static int _lastId;
    static Task()
    {
        _lastId = 0;
    }
    
    private int _id;
    public string Title;
    public bool IsDone = false;
    private DateTime _createdAt;

    public Task(string title)
    {
        _id = _lastId;
        _lastId++;
        Title = title;
        _createdAt = DateTime.Now;
    }
}