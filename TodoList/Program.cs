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