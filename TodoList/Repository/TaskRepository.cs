using TodoList.Models;

namespace TodoList.Repository;

class TaskRepository
{
    private readonly Dictionary<int, Todo> _taskList = new();
    
    public void Add<T>(T task) where T: Todo
    {
        _taskList.Add(Todo.LastId - 1, task);
    }

    public int GetCurrentTasksCount() => Todo.CurrentTasksCount;
    public int GetDoneTasksCount() => Todo.DoneTasksCount;

    public Todo? GetTaskById(int id)
    {
        if (_taskList.ContainsKey(id))
        {
            return _taskList[id];
        }
        return null;
    }

    public void DoneCurrentTask(int id)
    {
        if (_taskList.ContainsKey(id) && !_taskList[id].IsDone)
        {
            Todo.CurrentTasksCount--;
            Todo.DoneTasksCount++;
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
            Todo.DoneTasksCount--;
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

    public Todo? GetByIdOrDefault(int id)
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

    public void AddTags(Todo todo, string text)
    {
        var tagList = text.Split(',');
        foreach (var tag in tagList)
        {
            todo.Tags.Add(tag.Trim());
        }
    }

    public void DeleteTags(Todo todo, string text)
    {
        var tagList = text.Split(',');
        foreach (var tag in tagList)
        {
            todo.Tags.Remove(tag.Trim());
        }
    }

    public int GetTagCount(Todo todo)
    {
        return todo.Tags.Count;
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