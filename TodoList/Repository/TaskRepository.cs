using Task = TodoList.Models.Task;

namespace TodoList.Repository;

class TaskRepository
{
    private readonly Dictionary<int, Task> _taskList = new();

    public void Add<T>(T task) where T: Task
    {
        _taskList.Add(Task.LastId - 1, task);
    }

    public int GetCurrentTasksCount() => Task.CurrentTasksCount;
    public int GetDoneTasksCount() => Task.DoneTasksCount;

    public void DoneCurrentTask(int id)
    {
        if (_taskList.ContainsKey(id))
        {
            Task.CurrentTasksCount--;
            Task.DoneTasksCount++;
            _taskList[id].IsDone = true;
        }
        else
        {
            throw new Exception("Нет такой задачи");
        }
    }

    public void DeleteDoneTask(int id)
    {
        if (_taskList.ContainsKey(id) && _taskList[id].IsDone)
        {
            Task.DoneTasksCount--;
            _taskList.Remove(id);
        }
        else
        {
            throw new Exception("Нет такой задачи");
        }
    }
    
    public string GetCurrentTasksList()
    {
        var title = "";
        foreach (var task in _taskList)
        {
            if (!task.Value.IsDone)
            {
                title += Convert.ToString(task.Key) + ": " + task.Value + "\n";
            }
        }
        return title;
    }
    
    public string GetDoneTasksList()
    {
        var title = "";
        foreach (var task in _taskList)
        {
            if (task.Value.IsDone)
            {
                title += Convert.ToString(task.Key) + ": " + task.Value + "\n";
            }
        }                                                                                      
        return title;
    }

    public Task? GetByIdOrDefault(int id)
    {
        if (_taskList.ContainsKey(id))
        {
            return _taskList[id];
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
        foreach (var task in _taskList)
        {
            if (task.Value.Tags.Contains(tag) && !task.Value.IsDone)
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