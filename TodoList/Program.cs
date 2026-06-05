using TodoList.Services;

namespace TodoList;

class Program
{
    private static void Main()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.Clear();
        
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
                              5 - добавить теги
                              6 - удалить теги
                              7 - поиск по тегу
                              8 - получить статистику
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
                case 5:
                    taskService.AddTags();
                    Console.ReadKey();
                    break;
                case 6:
                    taskService.DeleteTags();
                    Console.ReadKey();
                    break;
                case 7:
                    taskService.FindByTag();
                    Console.ReadKey();
                    break;
                case 8:
                    taskService.GetStats();
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