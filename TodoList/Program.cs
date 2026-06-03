using TodoList.Services;

namespace TodoList;

class Program
{
    private static void Main()
    {
        int state;
        var taskService = new TaskService();

        do
        {
            taskService.ShowTasks();
            Console.WriteLine("""
                              Список доступных команд:
                              0 - выход
                              1 - добавить задачу
                              2 - выполнить задачу
                              3 - удалить задачу
                              """ + "\n");

            state = Convert.ToInt32(Console.ReadLine());

            switch (state)
            {
                case 0:
                    break;
                case 1:
                    taskService.AddTask();
                    Console.ReadKey();
                    break;
                case 2:
                    taskService.DoneTask();
                    Console.ReadKey();
                    break;
                case 3:
                    taskService.DeleteTask();
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Такой операции не существует\n"); 
                    Console.ReadKey();
                    break;
            }

            Console.Clear();

        } while (state != 0);
    }
}

// Цель: Сделать данные иммутабельными, использовать record и деконструкцию
// -----------------------------TodoItem переписан как record (бонус: реализация Equals/GetHashCode из коробки для поиска дубликатов) 
// -----------------------------При отметке IsDone используется with: items[index] = items[index] with { IsDone = true }; 
// Добавлен метод в TodoManager, возвращающий статистику: (int Total, int Completed, int Overdue) GetStats()
// В Main статистика принимается через деконструкцию: var (total, completed, overdue) = manager.GetStats();

// Используется record и выражение with
// Статистика возвращается в виде кортежа и деконструируется





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