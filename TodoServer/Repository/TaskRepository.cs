using TodoServer.Models;
using TodoServer.Exceptions;
using TodoServer.Repository.Interfaces;
using static TodoServer.Repository.StaticData;

namespace TodoServer.Repository;

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
        return TaskList.ContainsKey(id) ? TaskList[id] : null;
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
        return TaskList
            .Where(task => !task.Value.IsDone)
            .ToDictionary(task => task.Key, task => task.Value);
    } 
    
    public Dictionary<int, Todo> GetDoneTasksList()
    {
        return TaskList
            .Where(task => task.Value.IsDone)
            .ToDictionary(task => task.Key, task => task.Value);
    }

    public void SetPriority(Todo task, int priority)
    {
        task.Priority = priority;
    }
    
    public TodoStats GetStats()
    {
        int total = GetDoneTasksCount() + GetCurrentTasksCount(), completed = GetDoneTasksCount(), overdue = GetCurrentTasksCount();
        var stats = new TodoStats(total, completed, overdue);
        return stats;
    }
}