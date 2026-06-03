using TodoList.Models;
using Task = TodoList.Models.Task;
using TaskRepository = TodoList.Repository.TaskRepository;

namespace TodoList.Services;

internal class TaskService
{
    private readonly TaskRepository _taskRepository = new();
    
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
        
        string title;

        switch (state)
        {
            case 0:
                return;
            case 1: 
                Console.WriteLine("Введите описание задачи:");
                title = "" + Console.ReadLine();
                _taskRepository.Add(new Task(title));
                break;
            case 2:
                Console.WriteLine("Введите описание задачи:");
                title = "" + Console.ReadLine();
                Console.WriteLine("Введите дату в формате dd/mm/YYYY hh:mm:ss:");
                try
                {
                    var deadlineTime = DateTime.Parse("" + Console.ReadLine());
                    _taskRepository.Add(new DeadlinedTask(title, deadlineTime));
                }
                catch
                {
                    Console.WriteLine("Неправильно введена дата");
                    return;
                }
                break;
            case 3:
                Console.WriteLine("Введите описание задачи:");
                title = "" + Console.ReadLine();
                Console.WriteLine("Введи подзадачи:");
                List<string> subTasks = [];
                while (true)
                {
                    var subTask = "" + Console.ReadLine();
                    if (subTask is "")
                    {
                        break;
                    }
                    subTasks.Add(subTask);
                }
                _taskRepository.Add(new ChecklistTask(title,  subTasks));
                break;
            default:
                Console.WriteLine("Такой операции не существует\n"); 
                return;
        }
        Console.WriteLine("Таска добавлена\n");
    }

    public void DoneTask()
    {
        if (_taskRepository.GetCurrentTasksCount() == 0)
        {
            Console.WriteLine("Список задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер выполненной задачи:");

        if (int.TryParse(Console.ReadLine(), out var index))
        {
            _taskRepository.DoneCurrentTask(index);
            Console.WriteLine("Таска выполнена\n");
        }
        else
        {
            Console.WriteLine("Такой задачи не существует\n");
        }
        
    }
    
    public void DeleteTask()
    {
        if (_taskRepository.GetDoneTasksCount() == 0)
        {
            Console.WriteLine("Список выполненных задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер выполненной задачи:");
        
        if (int.TryParse(Console.ReadLine(), out var index))
        {
            _taskRepository.DeleteDoneTask(index);
            Console.WriteLine("Таска удалена\n");
        }
        else
        {
            Console.WriteLine("Такой задачи не существует\n");
        }
        
    }

    public void ShowTasks()
    {
        if (_taskRepository.GetCurrentTasksCount() != 0)
        {
            Console.WriteLine("Список текущих задач:");
            Console.WriteLine(_taskRepository.GetCurrentTasksList());
            Console.WriteLine("\n");
        }

        if (_taskRepository.GetDoneTasksCount() != 0)
        {
            Console.WriteLine("Список выполненных задач:");
            Console.WriteLine(_taskRepository.GetDoneTasksList());
            Console.WriteLine("\n");
        }
    }
}