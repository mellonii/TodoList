namespace TodoServer.Models;

internal class TodoStats()
{
    public readonly int TasksCount;
    public readonly int DoneTasksCount;
    public readonly int OverdueTasksCount;

    public TodoStats(int total, int completed, int overdue) : this()
    {
        TasksCount = total;
        DoneTasksCount = completed;
        OverdueTasksCount = overdue;
    }
}