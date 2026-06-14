using TodoList.Models;
using TodoList.Repository.Interfaces;

namespace TodoList.Repository;

public class TagRepository : ITagRepository
{
    public void AddTags(Todo todo, string text)
    {
        var tagList = text.Split(',');
        foreach (var tag in tagList)
        {
            todo.Tags.Add(tag.Trim());
        }
    }

    public void DeleteTags(Todo todo, string text)
    {
        var tagList = text.Split(',');
        foreach (var tag in tagList)
        {
            todo.Tags.Remove(tag.Trim());
        }
    }

    public int GetTagCount(Todo todo)
    {
        return todo.Tags.Count;
    }
    
    public string? FindByTag(string tag)
    {
        List<string> list = [];
        foreach (var task in StaticData.TaskList)
        {
            if (task.Value.Tags.Contains(tag) && !task.Value.IsDone)
            {
                list.Add(task.Value.Title);
            }
        }
        if (list.Count == 0)
        {
            return null;
        }
        return string.Join(", ", list);
    }
}