using TodoList.Exceptions;
using TodoList.Repository;
using TodoList.Repository.Interfaces;
using TodoList.Services.Interfaces;

namespace TodoList.Services;

internal class TagService(ITagRepository tagRepository) : ITagService
{
    public void AddTags()
    {
        if (TaskRepository.GetCurrentTasksCount() == 0 && TaskRepository.GetDoneTasksCount() == 0)
        {
            Console.WriteLine("Список задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер задачи, которой хотите добавить тег");
        if (!int.TryParse(Console.ReadLine(), out var id)) throw new TodoNotFoundException();
        
        var task = TaskRepository.GetByIdOrDefault(id);
        if (task is not null)
        {
            Console.WriteLine("Введите список тегов через запятую");
            var tags = "" + Console.ReadLine();
            if (tags == "") throw new EmptyReadLineException();
            
            tagRepository.AddTags(task, tags);
            Console.WriteLine("Теги добавлены\n");
        }
        else throw new TodoNotFoundException();
    }

    public void DeleteTags()
    {
        if (TaskRepository.GetCurrentTasksCount() == 0 && TaskRepository.GetDoneTasksCount() == 0)
        {
            Console.WriteLine("Список выполненных задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер задачи, у которой хотите удалить тег");
        if (!int.TryParse(Console.ReadLine(), out var id)) throw new TodoNotFoundException();

        var task = TaskRepository.GetByIdOrDefault(id);
        if (task is not null)
        {
            if (tagRepository.GetTagCount(task) == 0)
            {
                Console.WriteLine("У данной задачи нет тегов\n");
                return;
            }
            Console.WriteLine("Введите список тегов на удаление через запятую");
            var tags = "" + Console.ReadLine();
            if (tags == "") throw new EmptyReadLineException();
            
            tagRepository.DeleteTags(task, tags);
            Console.WriteLine("Теги удалены\n");
        }
        else throw new TodoNotFoundException();
    }

    public void FindByTag()
    {
        if (TaskRepository.GetCurrentTasksCount() == 0 && TaskRepository.GetDoneTasksCount() == 0)
        {
            Console.WriteLine("Список задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите тег");
        var tag = "" + Console.ReadLine();
        if (tag == "") throw new EmptyReadLineException();
        var text = tagRepository.FindByTag(tag);
        Console.WriteLine(text ?? "Задач с таким тегом нет");
    }
}