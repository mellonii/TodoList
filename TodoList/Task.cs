namespace TodoList;

internal class Task
{
    private static int _lastId;
    static Task()
    {
        _lastId = 0;
    }
    
    private int _id;
    protected readonly string Title;
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

internal class TodoManager
{
    // Создай класс TodoManager и интерфейс для него. TodoManager должен в себе иметь только методы оперирующие с data-классами из пунктов 1 и 2.
}

// Выдели ITodoRepository и TodoRepository который хранит в себе private readonly List<TodoItem>

// Доработай интерфейс консоли ввода задач под требования пунктов выше. Добавить возможность выбрать тип data-класса и добавить его