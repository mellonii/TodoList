namespace TodoList;

internal class Task
{
    private static int _lastId;
    static Task()
    {
        _lastId = 0;
    }
    
    private int _id;
    public string Title;
    public bool IsDone = false;
    private DateTime _createdAt;

    public Task(string title)
    {
        _id = _lastId;
        _lastId++;
        Title = title;
        _createdAt = DateTime.Now;
    }
    
    public override string ToString()
    {
        return Title;
    }
}

internal class DeadlinedTask : Task
{
    private DateTime _deadlineTime;
    
    public DeadlinedTask(string title, DateTime deadlineTime) : base(title)
    {
        _deadlineTime = deadlineTime;
    }
    
    public override string ToString()
    {
        return Title + " - " + _deadlineTime.ToString("dd.MM.yyyy HH:mm:ss");
    }
}

internal class ChecklistTask : Task
{ 
    private List<string> _subTasks = [];

    ChecklistTask(string title, List<string> subTasks) : base(title)
    {
        _subTasks = subTasks;
    }
}

internal class TodoManager
{
    // Создай класс TodoManager и интерфейс для него. TodoManager должен в себе иметь только методы оперирующие с data-классами из пунктов 1 и 2.
}



// Выдели ITodoRepository и TodoRepository который хранит в себе private readonly List<TodoItem>

// Переопредели в дата классах метод ToString() для красивого вывода в консоль. 

// Доработай интерфейс консоли ввода задач под требования пунктов выше. Добавить возможность выбрать тип data-класса и добавить его