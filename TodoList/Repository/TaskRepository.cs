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

    public static int GetCurrentTasksCount() => Todo.CurrentTasksCount;
    public static int GetDoneTasksCount() => Todo.DoneTasksCount;
    
    public static Todo? GetByIdOrDefault(int id)
    {
        if (TaskList.ContainsKey(id))
        {
            return TaskList[id];
        }
        return null;
    }

    public void DoneCurrentTask(int id)
    {
        if (!TaskList[id].IsDone)
        {
            Todo.CurrentTasksCount--;
            Todo.DoneTasksCount++;
            TaskList[id].IsDone = true;
        }
        else
        {
            throw new TodoNotFoundException();
        }
    }

    public void DeleteDoneTask(int id)
    {
        if (TaskList[id].IsDone)
        {
            Todo.DoneTasksCount--;
            TaskList.Remove(id);
        }
        else
        {
            throw new TodoNotFoundException();
        }
    }
    
    public string GetCurrentTasksList()
    {
        var title = "";
        foreach (var task in TaskList)
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
        foreach (var task in TaskList)
        {
            if (task.Value.IsDone)
            {
                title += Convert.ToString(task.Key) + ": " + task.Value + "\n";
            }
        }                                                                                      
        return title;
    }
    
    public (int, int, int) GetStats()
    {
        int total = GetDoneTasksCount() + GetCurrentTasksCount(), completed = GetDoneTasksCount(), overdue = GetCurrentTasksCount();
        return (total, completed, overdue);
    }
    
}