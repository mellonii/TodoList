using TodoList.Models;
using TodoList.Exceptions;
using TaskRepository = TodoList.Repository.TaskRepository;

namespace TodoList.Services;

internal class TaskService
{
    private readonly TaskRepository _taskRepository = new();
    private delegate void Message(string message, ConsoleColor color);
    private readonly Message _notify;

    public TaskService()
    {
        _notify += DisplayMessage;
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
            throw new InvalidTodoDataException("Была введена пустая строка");
        }
        _taskRepository.Add(new Todo(title));
    }

    private void AddDeadlinedTask()
    {
        Console.WriteLine("Введите описание задачи:");
        var title = "" + Console.ReadLine();
        if (title is "")
        {
            throw new InvalidTodoDataException("Была введена пустая строка");
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
            throw new InvalidTodoDataException("Была введена пустая строка");
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
        if (_taskRepository.GetCurrentTasksCount() == 0)
        {
            Console.WriteLine("Список задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер выполненной задачи:");

        if (int.TryParse(Console.ReadLine(), out var id))
        {
            var task = _taskRepository.GetByIdOrDefault(id);
            if (task is not null)
            {
                _taskRepository.DoneCurrentTask(id);
                _notify.Invoke($"Задача {task.Title} выполнена!!", ConsoleColor.Green);
            }
            else throw new TodoNotFoundException("Задачи с таким id нет");
        }
        else throw new InvalidTodoDataException("Неправильно введен id задачи");
    }
    
    public void DeleteTask()
    {
        if (_taskRepository.GetDoneTasksCount() == 0)
        {
            Console.WriteLine("Список выполненных задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер выполненной задачи:");
        
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            var task = _taskRepository.GetByIdOrDefault(id);
            if (task is not null)
            {
                _taskRepository.DeleteDoneTask(id);
                _notify.Invoke($"Задача {task.Title} удалена!!", ConsoleColor.Green);
            }
            else throw new TodoNotFoundException("Задачи с таким id нет");
        }
        else throw new InvalidTodoDataException("Неправильно введен id задачи");
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

    public void AddTags()
    {
        if (_taskRepository.GetCurrentTasksCount() == 0 && _taskRepository.GetDoneTasksCount() == 0)
        {
            Console.WriteLine("Список задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер задачи, которой хотите добавить тег");
        if (!int.TryParse(Console.ReadLine(), out var id)) throw new TodoNotFoundException("Задачи с таким id нет");
        
        var task = _taskRepository.GetByIdOrDefault(id);
        if (task is not null)
        {
            Console.WriteLine("Введите список тегов через запятую");
            var tags = "" + Console.ReadLine();
            if (tags == "") throw new InvalidTodoDataException("Была введена пустая строка");
            
            _taskRepository.AddTags(task, tags);
            Console.WriteLine("Теги добавлены\n");
        }
        else throw new TodoNotFoundException("Задачи с таким id нет");
    }

    public void DeleteTags()
    {
        if (_taskRepository.GetCurrentTasksCount() == 0 && _taskRepository.GetDoneTasksCount() == 0)
        {
            Console.WriteLine("Список выполненных задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер задачи, у которой хотите удалить тег");
        if (!int.TryParse(Console.ReadLine(), out var id)) throw new TodoNotFoundException("Задачи с таким id нет");

        var task = _taskRepository.GetByIdOrDefault(id);
        if (task is not null)
        {
            if (_taskRepository.GetTagCount(task) == 0)
            {
                Console.WriteLine("У данной задачи нет тегов\n");
                return;
            }
            Console.WriteLine("Введите список тегов на удаление через запятую");
            var tags = "" + Console.ReadLine();
            if (tags == "") throw new InvalidTodoDataException("Была введена пустая строка");
            
            _taskRepository.DeleteTags(task, tags);
            Console.WriteLine("Теги удалены\n");
        }
        else throw new TodoNotFoundException("Задачи с таким id нет");
    }

    public void FindByTag()
    {
        if (_taskRepository.GetCurrentTasksCount() == 0 && _taskRepository.GetDoneTasksCount() == 0)
        {
            Console.WriteLine("Список задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите тег");
        var tag = "" + Console.ReadLine();
        if (tag == "") throw new InvalidTodoDataException("Была введена пустая строка");
        var text = _taskRepository.FindByTag(tag);
        if (text is not null)
        {
            Console.WriteLine(text);
        }
        else
        {
            Console.WriteLine("Задач с таким тегом нет");
        }
    }

    public void GetStats()
    {
        var (total, completed, overdue) = _taskRepository.GetStats();
        Console.WriteLine($"Всего задач: {total}, выполнено: {completed}, осталось: {overdue}");
    }
    
}