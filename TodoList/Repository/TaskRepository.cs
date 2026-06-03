using Task = TodoList.Models.Task;

namespace TodoList.Repository;

class TaskRepository
{
    private Dictionary<int, Task> _currentTasksList = new();
    private Dictionary<int, Task> _doneTasksList = new();

    public void Add<T>(T task) where T: Task
    {
        _currentTasksList.Add(Task.LastId - 1, task);
    }

    public int GetCurrentTasksCount() => _currentTasksList.Count;
    public int GetDoneTasksCount() => _doneTasksList.Count;

    public void DoneCurrentTask(int index)
    {
        _doneTasksList.Add(index, _currentTasksList[index]);
        _currentTasksList[index].IsDone = true;
        _currentTasksList.Remove(index);
    }

    public void DeleteDoneTask(int index)
    {
        if (_doneTasksList.ContainsKey(index))
        {
            _doneTasksList.Remove(index);
        }
        else
        {
            throw new Exception("Нет такой задачи");
        }
    }
    
    public string GetCurrentTasksList()
    {
        var title = "";
        foreach (var task in _currentTasksList)
        {
            title += Convert.ToString(task.Key) + ": " + task.Value + "\n";
        }
        return title;
    }
    
    public string GetDoneTasksList()
    {
        var title = "";
        foreach (var task in _doneTasksList)
        {
            title += Convert.ToString(task.Key) + ": " + task.Value + "\n";
        }
        return title;
    }
}

// Цель: Оптимизировать хранение, обеспечить быстрый поиск по ID и работу с тегами
// Внутри TodoManager (TaskService) List<TodoItem> заменен на Dictionary<int, TodoItem>
// ID генерируется автоматически (инкремент)
// Добавлена система тегов: в TodoItem (Task) добавлено свойство HashSet<string> Tags
// Поиск по ID и по тегам
// Команда find для поиска задач, содержащих хотя бы один из введенных тегов (пересечение множеств)

// Удаление и поиск по ID работают за O(1) благодаря Dictionary
// К задаче можно привязать несколько тегов и осуществить по ним поиск


// TaskRepository класс в отдельной папке, хранит  словарь с Dictionary<int, TodoItem>
// метод Add принимает генерик и принимает Task
// GetById FindByTag там
// вынести туда задачи и все кроме выводов и вводов