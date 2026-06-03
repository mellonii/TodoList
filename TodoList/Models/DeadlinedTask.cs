namespace TodoList.Models;

internal class DeadlinedTask : Task
{
    private readonly DateTime _deadlineTime;
    
    public DeadlinedTask(string title, DateTime deadlineTime) : base(title)
    {
        _deadlineTime = deadlineTime;
    }
    
    public override string ToString()
    {
        return base.ToString() + " - " + _deadlineTime.ToString("dd.MM.yyyy HH:mm:ss");
    }
}