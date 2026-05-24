namespace TodoList;

internal class Tasks
{
    private readonly List<string> _tasksList = [];
    
    public void AddTask()
    {
        Console.WriteLine("Введите описание задачи:");
        _tasksList.Add("" + Console.ReadLine());
        Console.WriteLine("Таска добавлена\n");
    }

    public void DeleteTask()
    {
        if (_tasksList.Count == 0)
        {
            Console.WriteLine("Список задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер выполненной задачи:");

        try
        {
            var index = Convert.ToInt32(Console.ReadLine()) - 1;
            
            if (index >= 0 && index < _tasksList.Count)
            {
                _tasksList.RemoveAt(index);
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
        if (_tasksList.Count == 0)
        {
            Console.WriteLine("Список задач пуст\n");
            return;
        }
        
        Console.WriteLine("Список задач:");
        for (var i = 0; i < _tasksList.Count; i++)
        {
            Console.WriteLine(i + 1 + ": " + _tasksList[i]);
        }
        Console.WriteLine("\n");
    }
}