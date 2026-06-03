namespace TodoList.Models;

internal class ChecklistTask : Task
{ 
    private readonly List<string> _subTasks;

    public ChecklistTask(string title, List<string> subTasks) : base(title)
    {
        _subTasks = subTasks;
    }

    public override string ToString()
    {
        var text = Title + "\n";
        foreach (var subTask in _subTasks)
        {
            text += "\t> " + subTask + "\n";
        }
        return text;
    }
}