using TodoServer.Models;
using TodoServer.Repository.Interfaces;

namespace TodoServer.Repository;

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
    
    public Dictionary<int, Todo> FindByTag(string tag)
    {
        return StaticData.TaskList
            .Where(task => task.Value.Tags.Contains(tag) && !task.Value.IsDone)
            .ToDictionary(task => task.Key, task => task.Value);
    }
}