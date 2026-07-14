using TodoServer.Exceptions;
using TodoServer.Repository.Interfaces;
using TodoServer.Services.Interfaces;

namespace TodoServer.Services;

internal class TagService(ITagRepository tagRepository, ITaskRepository taskRepository)
    : ITagService
{
    public void AddTags(int id, string tags)
    {
        var task = taskRepository.GetByIdOrDefault(id);
        if (task is null) throw new TodoNotFoundException();
        if (tags == "") throw new EmptyReadLineException();
        tagRepository.AddTags(task, tags);
    }

    public void DeleteTags(int id, string tags)
    {
        var task = taskRepository.GetByIdOrDefault(id);
        if (task is null) throw new TodoNotFoundException();
        if (tags == "") throw new EmptyReadLineException();
        tagRepository.DeleteTags(task, tags);
    }

    public string FindByTag(string tag)
    {
        if (tag == "") throw new EmptyReadLineException();
        var todoList = tagRepository.FindByTag(tag);
        return string.Join(", ", todoList.Select(task => task.Value.Title));
    }
    
    public string GetTagsById(int id)
    {
        var task = taskRepository.GetByIdOrDefault(id);
        return task is not null ? string.Join(", ", task.Tags) : "";
    }

    public string GetTags()
    {
        var tags = "";
        if (taskRepository.GetCurrentTasksCount() != 0)
        {
            tags += string.Join(", ", taskRepository.GetCurrentTasksList().Select(task => task.Value.Tags));
        }
        if (taskRepository.GetDoneTasksCount() != 0)
        {
            tags += string.Join(", ", taskRepository.GetDoneTasksList().Select(task => task.Value.Tags));
        }
        return tags;
    }
}