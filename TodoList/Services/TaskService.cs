using TodoList.Models;
using Task = TodoList.Models.Task;
using TaskRepository = TodoList.Repository.TaskRepository;

namespace TodoList.Services;

internal class TaskService
{
    private readonly TaskRepository _taskRepository = new();
    
    public event Action<Task>? TaskCompleted;
    private void MarkDone(Task task)
    {
        TaskCompleted?.Invoke(task);
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
        Console.WriteLine("Таска добавлена\n");
    }

    private void AddSimpleTask()
    {
        Console.WriteLine("Введите описание задачи:");
        var title = "" + Console.ReadLine();
        _taskRepository.Add(new Task(title));
    }

    private void AddDeadlinedTask()
    {
        Console.WriteLine("Введите описание задачи:");
        var title = "" + Console.ReadLine();
        while (true)
        {
            Console.WriteLine("Введите дату в формате dd/mm/YYYY hh:mm:ss");
            try
            {
                var deadlineTime = DateTimeOffset.Parse("" + Console.ReadLine());
                _taskRepository.Add(new DeadlinedTask(title, deadlineTime));
                break;
            }
            catch
            {
                Console.WriteLine("Неправильно введена дата");
            }
        }
    }

    private void AddChecklistTask()
    {
        Console.WriteLine("Введите описание задачи:");
        var title = "" + Console.ReadLine();
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

        if (int.TryParse(Console.ReadLine(), out var index))
        {
            try
            {
                _taskRepository.DoneCurrentTask(index);
                MarkDone(_taskRepository.GetTaskById(index));
            }
            catch
            {
                Console.WriteLine("Такой задачи не существует\n");
            }
        }
        else
        {
            Console.WriteLine("Неправильно введен номер задачи\n");
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
            try
            {
                _taskRepository.DeleteDoneTask(index);
                Console.WriteLine("Таска удалена\n");
            }
            catch
            {
                Console.WriteLine("Такой задачи не существует\n");
            }
            
        }
        else
        {
            Console.WriteLine("Неправильно введен номер задачи\n");
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

    public void AddTags()
    {
        Console.WriteLine("Введите номер задачи, которой хотите добавить тег");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Такой задачи не существует\n");
            return;
        }

        var task = _taskRepository.GetByIdOrDefault(id);
        if (task is not null)
        {
            Console.WriteLine("Введите список тегов через запятую");
            var tags = "" + Console.ReadLine();
            if (tags == "")
            {
                Console.WriteLine("Вы ввели пустую строку");
                return;
            }
            _taskRepository.AddTags(task, tags);
            Console.WriteLine("Теги добавлены\n");
        }
        else
        {
            Console.WriteLine("Такой задачи не существует\n");
        }
    }

    public void DeleteTags()
    {
        Console.WriteLine("Введите номер задачи, у которой хотите удалить тег");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Такой задачи не существует\n");
            return;
        }

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
            if (tags == "")
            {
                Console.WriteLine("Вы ввели пустую строку");
                return;
            }
            _taskRepository.DeleteTags(task, tags);
            Console.WriteLine("Теги удалены\n");
        }
        else
        {
            Console.WriteLine("Такой задачи не существует\n");
        }
    }

    public void FindByTag()
    {
        Console.WriteLine("Введите тег");
        var tag = "" + Console.ReadLine();
        if (tag == "")
        {
            Console.WriteLine("Вы ввели пустую строку");
            return;
        }
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