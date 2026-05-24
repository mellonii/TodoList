namespace TodoList;

class Program
{
    private static void Main()
    { 
        int state;
        var tasks = new Tasks();

        do
        {
            Console.WriteLine("""
                              Список доступных команд:
                              0 - выход
                              1 - добавить задачу
                              2 - выполнить задачу
                              3 - просмотр задач
                              """ + "\n");
            
            state = Convert.ToInt32(Console.ReadLine()); 
            
            switch (state)
            {
                case 0: 
                    break;
                case 1: 
                    tasks.AddTask(); 
                    Console.ReadKey(); 
                    break;
                case 2: 
                    tasks.DeleteTask(); 
                    Console.ReadKey(); 
                    break;
                case 3: 
                    tasks.ShowTasks(); 
                    Console.ReadKey(); 
                    break;
                default: 
                    Console.WriteLine("Такой операции не существует\n"); break;
            }
            
            Console.Clear();
            
        } while (state != 0);
    }
}

     