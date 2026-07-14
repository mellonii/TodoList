namespace TodoServer.Models;

internal class ChecklistTask : Todo
{ 
    private readonly List<string> _subTasks;

    public ChecklistTask(string title, List<string> subTasks) : base(title)
    {
        _subTasks = subTasks;
    }

    public override string ToString()
    {
        return _subTasks.Aggregate(base.ToString() + "\n", (tmp, subTask) => tmp + "\t> " + subTask + "\n");
    }
}