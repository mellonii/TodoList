using TodoList.Models;
using TodoList.Exceptions;
using TodoList.Repository;
using TodoList.Repository.Interfaces;
using TodoList.Services.Interfaces;

namespace TodoList.Services;

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
    
    public void AddTask()
    {
        Console.WriteLine("\n" + """
                          Выбери тип создаваемой задачи:
                          1 - обычная
                          2 - с дедлайном
                          3 - с подзадачами
                          0 - вернуться
                          """ + "\n");

        if (!int.TryParse(Console.ReadLine(), out var state))
        {
            Console.WriteLine("Такой операции не существует\n");
            return;
        }

        switch (state)
        {
            case 0:
                return;
            case 1:
                AddSimpleTask();
                break;
            case 2:
                AddDeadlinedTask();
                break;
            case 3:
                AddChecklistTask();
                break;
            default:
                Console.WriteLine("Такой операции не существует\n"); 
                return;
        }
        _notify.Invoke($"Задача добавлена!!", ConsoleColor.White);
    }

    private void AddSimpleTask()
    {
        Console.WriteLine("Введите описание задачи:");
        var title = "" + Console.ReadLine();
        if (title is "")
        {
            throw new EmptyReadLineException();
        }
        _taskRepository.Add(new Todo(title));
    }

    private void AddDeadlinedTask()
    {
        Console.WriteLine("Введите описание задачи:");
        var title = "" + Console.ReadLine();
        if (title is "")
        {
            throw new EmptyReadLineException();
        }
        while (true)
        {
            Console.WriteLine("Введите дату в формате dd/mm/YYYY hh:mm:ss");
            if (!DateTimeOffset.TryParse("" + Console.ReadLine(), out var deadlineTime))
            {
                Console.WriteLine("Неправильно введена дата");
            }
            else
            {
                _taskRepository.Add(new DeadlinedTask(title, deadlineTime));
                break;
            }
        }
    }

    private void AddChecklistTask()
    {
        Console.WriteLine("Введите описание задачи:");
        var title = "" + Console.ReadLine();
        if (title is "")
        {
            throw new EmptyReadLineException();
        }
        Console.WriteLine("Введи подзадачи:");
        List<string> subTasks = [];
        while (true)
        {
            var subTask = "" + Console.ReadLine();
            if (subTask.Trim() is "")
            {
                break;
            }
            subTasks.Add(subTask);
        }
        _taskRepository.Add(new ChecklistTask(title,  subTasks));
    }

    public void DoneTask()
    {
        if (TaskRepository.GetCurrentTasksCount() == 0)
        {
            Console.WriteLine("Список задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер выполненной задачи:");

        if (int.TryParse(Console.ReadLine(), out var id))
        {
            var task = TaskRepository.GetByIdOrDefault(id);
            if (task is not null)
            {
                _taskRepository.DoneCurrentTask(id);
                _notify.Invoke($"Задача {task.Title} выполнена!!", ConsoleColor.Green);
            }
            else throw new TodoNotFoundException();
        }
        else throw new InvalidTodoDataException();
    }
    
    public void DeleteTask()
    {
        if (TaskRepository.GetDoneTasksCount() == 0)
        {
            Console.WriteLine("Список выполненных задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер выполненной задачи:");
        
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            var task = TaskRepository.GetByIdOrDefault(id);
            if (task is not null)
            {
                _taskRepository.DeleteDoneTask(id);
                _notify.Invoke($"Задача {task.Title} удалена!!", ConsoleColor.Green);
            }
            else throw new TodoNotFoundException();
        }
        else throw new InvalidTodoDataException();
    }

    public void ShowTasks()
    {
        if (TaskRepository.GetCurrentTasksCount() != 0)
        {
            Console.WriteLine("Список текущих задач:");
            Console.WriteLine(_taskRepository.GetCurrentTasksList());
            Console.WriteLine("\n");
        }

        if (TaskRepository.GetDoneTasksCount() != 0)
        {
            Console.WriteLine("Список выполненных задач:");
            Console.WriteLine(_taskRepository.GetDoneTasksList());
            Console.WriteLine("\n");
        }
    }

    public void GetStats()
    {
        var (total, completed, overdue) = _taskRepository.GetStats();
        Console.WriteLine($"Всего задач: {total}, выполнено: {completed}, осталось: {overdue}");
    }
    
}