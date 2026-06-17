namespace TodoList.Models;

internal class DeadlinedTask : Todo
{
    private readonly DateTimeOffset _deadlineTime;
    
    public DeadlinedTask(string title, DateTimeOffset deadlineTime) : base(title)
    {
        _deadlineTime = deadlineTime;
    }
    
    public override string ToString()
    {
        return base.ToString() + " - " + _deadlineTime.ToLocalTime().ToString();
    }
}