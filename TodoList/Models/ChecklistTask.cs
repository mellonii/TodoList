namespace TodoList.Models;

internal class ChecklistTask : Todo
{ 
    private readonly List<string> _subTasks;

    public ChecklistTask(string title, List<string> subTasks) : base(title)
    {
        _subTasks = subTasks;
    }

    public override string ToString()
    {
        var text = base.ToString() + "\n";
        foreach (var subTask in _subTasks)
        {
            text += "\t> " + subTask + "\n";
        }
        return text;
    }
}