namespace TodoList;

internal class TaskService
{
    private readonly List<Task> _currentTasksList = [];
    private readonly List<Task> _doneTasksList = [];
    
    public void AddTask()
    {
        Console.WriteLine("Введите описание задачи:");
        _currentTasksList.Add(new Task("" + Console.ReadLine()));
        Console.WriteLine("Таска добавлена\n");
    }

    public void DoneTask()
    {
        if (_currentTasksList.Count == 0)
        {
            Console.WriteLine("Список задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер выполненной задачи:");

        try
        {
            var index = Convert.ToInt32(Console.ReadLine()) - 1;
            
            if (index >= 0 && index < _currentTasksList.Count)
            {
                _doneTasksList.Add(_currentTasksList[index]);
                _currentTasksList[index].IsDone = true;
                _currentTasksList.RemoveAt(index);
                Console.WriteLine("Таска выполнена\n");
            }
            else
            {
                Console.WriteLine("Такой задачи не существует\n");
            }
        }
        catch
        {
            Console.WriteLine("Такой задачи не существует\n");
        }
        
    }
    
    public void DeleteTask()
    {
        if (_doneTasksList.Count == 0)
        {
            Console.WriteLine("Список выполненных задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер выполненной задачи:");

        try
        {
            var index = Convert.ToInt32(Console.ReadLine()) - 1;
            
            if (index >= 0 && index < _doneTasksList.Count)
            {
                _doneTasksList.RemoveAt(index);
                Console.WriteLine("Таска удалена\n");
            }
            else
            {
                Console.WriteLine("Такой задачи не существует\n");
            }
        }
        catch
        {
            Console.WriteLine("Такой задачи не существует\n");
        }
        
    }

    public void ShowTasks()
    {
        if (_currentTasksList.Count != 0)
        {
            Console.WriteLine("Список текущих задач:");
            for (var i = 0; i < _currentTasksList.Count; i++)
            {
                Console.WriteLine(i + 1 + ": " + _currentTasksList[i].Title);
            }
            Console.WriteLine("\n");
        }

        if (_doneTasksList.Count != 0)
        {
            Console.WriteLine("Список выполненных задач:");
            for (var i = 0; i < _doneTasksList.Count; i++)
            {
                Console.WriteLine(i + 1 + ": " + _doneTasksList[i].Title);
            }
            Console.WriteLine("\n");
        }
    }
}