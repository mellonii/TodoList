using Task = TodoList.Models.Task;

namespace TodoList.Repository;

class TaskRepository
{
    private readonly Dictionary<int, Task> _currentTasksList = new();
    private readonly Dictionary<int, Task> _doneTasksList = new();

    public void Add<T>(T task) where T: Task
    {
        _currentTasksList.Add(Task.LastId - 1, task);
    }

    public int GetCurrentTasksCount() => _currentTasksList.Count;
    public int GetDoneTasksCount() => _doneTasksList.Count;

    public void DoneCurrentTask(int id)
    {
        _doneTasksList.Add(id, _currentTasksList[id]);
        _currentTasksList[id].IsDone = true;
        _currentTasksList.Remove(id);
    }

    public void DeleteDoneTask(int id)
    {
        if (_doneTasksList.ContainsKey(id))
        {
            _doneTasksList.Remove(id);
        }
        else
        {
            throw new Exception("Нет такой задачи");
        }
    }
    
    public string GetCurrentTasksList()
    {
        var title = "";
        foreach (var task in _currentTasksList)
        {
            title += Convert.ToString(task.Key) + ": " + task.Value + "\n";
        }
        return title;
    }
    
    public string GetDoneTasksList()
    {
        var title = "";
        foreach (var task in _doneTasksList)
        {
            title += Convert.ToString(task.Key) + ": " + task.Value + "\n";
        }                                                                                      
        return title;
    }

    public Task? GetByIdOrDefault(int id)
    {
        if (_currentTasksList.ContainsKey(id))
        {
            return _currentTasksList[id];
        }
        else if (_doneTasksList.ContainsKey(id))
        {
            return _doneTasksList[id];
        }
        else
        {
            return null;
        }
    }

    public void AddTags(Task task, string text)
    {
        var tagList = text.Split(',');
        foreach (var tag in tagList)
        {
            task.Tags.Add(tag.Trim());
        }
    }

    public void DeleteTags(Task task, string text)
    {
        var tagList = text.Split(',');
        foreach (var tag in tagList)
        {
            task.Tags.Remove(tag.Trim());
        }
    }

    public int GetTagCount(Task task)
    {
        return task.Tags.Count;
    }
    
    public string? FindByTag(string tag)
    {
        List<string> list = [];
        foreach (var task in _currentTasksList)
        {
            if (task.Value.Tags.Contains(tag))
            {
                list.Add(task.Value.Title);
            }
        }
        if (list.Count == 0)
        {
            return null;
        }
        return string.Join(", ", list);
    }

    public (int, int, int) GetStats()
    {
        int total = GetDoneTasksCount() + GetCurrentTasksCount(), completed = GetDoneTasksCount(), overdue = GetCurrentTasksCount();
        return (total, completed, overdue);
    }
    
}