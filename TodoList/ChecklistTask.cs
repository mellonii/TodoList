namespace TodoList;

internal class ChecklistTask : Task
{ 
    private List<string> _subTasks = [];

    public ChecklistTask(string title, List<string> subTasks) : base(title)
    {
        _subTasks = subTasks;
    }

    public override string ToString()
    {
        string text = Title + "\n";
        foreach (string subTask in _subTasks)
        {
            text += "\t> " + subTask + "\n";
        }
        return text;
    }
}