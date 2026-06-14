using TodoList.Models;
using TodoList.Exceptions;
using TodoList.Repository.Interfaces;
using static TodoList.Repository.StaticData;

namespace TodoList.Repository;

internal class TaskRepository : ITaskRepository
{
    public void Add<T>(T task) where T: Todo
    {
        TaskList.Add(Todo.LastId - 1, task);
    }

    public int GetCurrentTasksCount() => Todo.CurrentTasksCount;
    public int GetDoneTasksCount() => Todo.DoneTasksCount;
    
    public bool EnsureTaskNotEmpty() => TaskList.Count != 0;
    
    public Todo? GetByIdOrDefault(int id)
    {
        if (TaskList.ContainsKey(id))
        {
            return TaskList[id];
        }
        return null;
    }

    public void DoneCurrentTask(int id)
    {
        var task = GetByIdOrDefault(id);
        if (task is null || TaskList[id].IsDone) throw new TodoNotFoundException();
        Todo.CurrentTasksCount--;
        Todo.DoneTasksCount++;
        TaskList[id].IsDone = true;
    }

    public string DeleteDoneTask(int id)
    {
        var task = GetByIdOrDefault(id);
        if (task is null || !TaskList[id].IsDone) throw new TodoNotFoundException();
        Todo.DoneTasksCount--;
        var title = task.Title;
        TaskList.Remove(id);
        return title;
    }
    
    public Dictionary<int, Todo> GetCurrentTasksList()
    {
        Dictionary<int, Todo> currentTaskList = new();
        foreach (var (key, task) in TaskList)
        {
            if (!task.IsDone)
            {
                currentTaskList.Add(key, task);
            }
        }
        return currentTaskList;
    } 
    
    public Dictionary<int, Todo> GetDoneTasksList()
    {
        Dictionary<int, Todo> doneTaskList = new();
        foreach (var (key, task) in TaskList)
        {
            if (task.IsDone)
            {
                doneTaskList.Add(key, task);
            }
        }
        return doneTaskList;
    }

    public bool SetPriority(Todo task, int priority)
    {
        if (priority is < 0 or > 3) return false;
        task.Priority = priority;
        return true;
    }
    
    public TodoStats GetStats()
    {
        int total = GetDoneTasksCount() + GetCurrentTasksCount(), completed = GetDoneTasksCount(), overdue = GetCurrentTasksCount();
        var stats = new TodoStats(total, completed, overdue);
        return stats;
    }
}