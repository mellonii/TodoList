using TodoServer.Exceptions;
using TodoServer.Repository.Interfaces;
using TodoServer.Services.Interfaces;

namespace TodoServer.Services;

internal class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;
    private readonly ITaskRepository _taskRepository;
    
    public TagService(ITagRepository tagRepository, ITaskRepository taskRepository)
    {
        _tagRepository =  tagRepository;
        _taskRepository = taskRepository;
    }
    
    public void AddTags()
    {
        if (!_taskRepository.EnsureTaskNotEmpty())
        {
            Console.WriteLine("Список задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер задачи, которой хотите добавить тег");
        if (!int.TryParse(Console.ReadLine(), out var id)) throw new TodoNotFoundException();
        
        var task = _taskRepository.GetByIdOrDefault(id);
        if (task is not null)
        {
            Console.WriteLine("Введите список тегов через запятую");
            var tags = "" + Console.ReadLine();
            if (tags == "") throw new EmptyReadLineException();
            
            _tagRepository.AddTags(task, tags);
            Console.WriteLine("Теги добавлены\n");
        }
        else throw new TodoNotFoundException();
    }

    public void DeleteTags()
    {
        if (!_taskRepository.EnsureTaskNotEmpty())
        {
            Console.WriteLine("Список выполненных задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите номер задачи, у которой хотите удалить тег");
        if (!int.TryParse(Console.ReadLine(), out var id)) throw new TodoNotFoundException();

        var task = _taskRepository.GetByIdOrDefault(id);
        if (task is not null)
        {
            if (_tagRepository.GetTagCount(task) == 0)
            {
                Console.WriteLine("У данной задачи нет тегов\n");
                return;
            }
            Console.WriteLine("Введите список тегов на удаление через запятую");
            var tags = "" + Console.ReadLine();
            if (tags == "") throw new EmptyReadLineException();
            
            _tagRepository.DeleteTags(task, tags);
            Console.WriteLine("Теги удалены\n");
        }
        else throw new TodoNotFoundException();
    }

    public void FindByTag()
    {
        if (!_taskRepository.EnsureTaskNotEmpty())
        {
            Console.WriteLine("Список задач пуст\n");
            return;
        }
        
        Console.WriteLine("Введите тег");
        var tag = "" + Console.ReadLine();
        if (tag == "") throw new EmptyReadLineException();
        var todoList = _tagRepository.FindByTag(tag);
        if (todoList.Count == 0) Console.WriteLine("Задач с таким тегом нет");
        
        List<string> list = [];
        foreach (var task in todoList)
        {
            list.Add(task.Value.Title);
        }
        Console.WriteLine(string.Join(", ", list));
    }
}