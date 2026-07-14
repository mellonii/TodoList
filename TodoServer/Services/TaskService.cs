using TodoServer.Models;
using TodoServer.Exceptions;
using TodoServer.Repository.Interfaces;
using TodoServer.Services.Interfaces;

namespace TodoServer.Services;

internal class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private delegate void Message(string message, ConsoleColor color);
    private readonly Message _notify;

    public TaskService(ITaskRepository taskRepository)
    {
        _notify += DisplayMessage;
        _taskRepository = taskRepository;
    }
    
    private static void DisplayMessage(string message, ConsoleColor color)
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = originalColor;
    }
    
    public void AddSimpleTask(string title)
    {
        if (title is "") throw new EmptyReadLineException();
        _taskRepository.Add(new Todo(title));
    }

    public void AddDeadlinedTask(string title, DateTimeOffset time)
    {
        if (title is "") throw new EmptyReadLineException();
        _taskRepository.Add(new DeadlinedTask(title, time));
    }

    public void AddChecklistTask(string title, string text)
    {
        if (title is "") throw new EmptyReadLineException();
        var subTasks = text.Split(',').Select(t => t.Trim()).ToList();
        _taskRepository.Add(new ChecklistTask(title, subTasks));
    }

    public void DoneTask(int id)
    {
        _taskRepository.DoneCurrentTask(id);
        _notify.Invoke($"Задача {_taskRepository.GetByIdOrDefault(id).Title} выполнена!!", ConsoleColor.Green);
    }
    
    public void DeleteTask(int id)
    {
        var title = _taskRepository.DeleteDoneTask(id);
        _notify.Invoke($"Задача {title} удалена!!", ConsoleColor.Green);
    }

    public string GetTasks()
    {
        return GetCurrentTasks() + GetDoneTasks();
    }

    public string GetCurrentTasks()
    {
        var tasks = "";
        if (_taskRepository.GetCurrentTasksCount() == 0) return tasks;
        
        tasks += "Список текущих задач:\n";
        tasks += _taskRepository.GetCurrentTasksList().Aggregate("", (tmp, pair) => tmp + pair.Key + ": " + pair.Value + "\n");
        tasks += "\n";
        return tasks;
    }

    public string GetDoneTasks()
    {
        var tasks = "";
        if (_taskRepository.GetDoneTasksCount() == 0) return tasks;
        
        tasks += "Список выполненных задач:\n";
        tasks += _taskRepository.GetDoneTasksList().Aggregate("", (tmp, pair) => tmp + pair.Key + ": " + pair.Value + "\n");
        tasks += "\n";
        return tasks;
    }

    public void SetPriority(int id, int priority)
    {
        if (priority is < 0 or > 3) return;
        var task = _taskRepository.GetByIdOrDefault(id);
        if (task is null) throw new TodoNotFoundException();
        _taskRepository.SetPriority(task, priority);
    }

    public string GetStats()
    {
        var stats = _taskRepository.GetStats();
        return $"Всего задач: {stats.TasksCount}, выполнено: {stats.DoneTasksCount}, осталось: {stats.OverdueTasksCount}";
    }

    public string GetTaskById(int id)
    {
        var task = _taskRepository.GetByIdOrDefault(id);
        return task is not null ? task.ToString() : "";
    }

    public string GetPriorityById(int id)
    {
        var task = _taskRepository.GetByIdOrDefault(id);
        return task is not null ? task.Priority.ToString() : "";
    }
}