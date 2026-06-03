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
                              1 - выход
                              2 - добавить задачу
                              3 - выполнить задачу
                              4 - удалить задачу
                              """ + "\n");

            if (!int.TryParse(Console.ReadLine(), out state))
            {
                Console.WriteLine("Такой операции не существует\n");
                Console.Clear();
                continue;
            }
            
            switch (state)
            {
                case 1:
                    break;
                case 2:
                    taskService.AddTask();
                    Console.ReadKey();
                    break;
                case 3:
                    taskService.DoneTask();
                    Console.ReadKey();
                    break;
                case 4:
                    taskService.DeleteTask();
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Такой операции не существует\n"); 
                    Console.ReadKey();
                    break;
            }

            Console.Clear();

        } while (state != 1);
    }
}

// Цель: Сделать данные иммутабельными, использовать record и деконструкцию
// -----------------------------TodoItem переписан как record (бонус: реализация Equals/GetHashCode из коробки для поиска дубликатов) 
// -----------------------------При отметке IsDone используется with: items[index] = items[index] with { IsDone = true }; 
// Добавлен метод в TodoManager, возвращающий статистику: (int Total, int Completed, int Overdue) GetStats()
// В Main статистика принимается через деконструкцию: var (total, completed, overdue) = manager.GetStats();

// Используется record и выражение with
// Статистика возвращается в виде кортежа и деконструируется