using System.Text;
using TodoList.Services;
using TodoList.Exceptions;
using TodoList.Repository;

namespace TodoList;

class Program
{
    private static void HandleErrors(Action func)
    {
        try
        {
            func();
        }
        catch (InvalidTodoDataException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (TodoNotFoundException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    
    private static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.Clear();
        
        int state;
        var taskRepository = new TaskRepository();
        var tagRepository = new TagRepository();
        var tagService = new TagService(tagRepository);
        var taskService = new TaskService(taskRepository);
        
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
                              9 - добавить приоритет
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
                    HandleErrors(taskService.AddTask);
                    Console.ReadKey();
                    break;
                case 3:
                    HandleErrors(taskService.DoneTask);
                    Console.ReadKey();
                    break;
                case 4:
                    HandleErrors(taskService.DeleteTask);
                    Console.ReadKey();
                    break;
                case 5:
                    HandleErrors(tagService.AddTags);
                    Console.ReadKey();
                    break;
                case 6:
                    HandleErrors(tagService.DeleteTags);
                    Console.ReadKey();
                    break;
                case 7:
                    tagService.FindByTag();
                    Console.ReadKey();
                    break;
                case 8:
                    taskService.GetStats();
                    Console.ReadKey();
                    break;
                case 9:
                    HandleErrors(taskService.SetPriority);
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