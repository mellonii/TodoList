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
        Dictionary<int, Todo> list = new();
        foreach (var (id, task) in StaticData.TaskList)
        {
            if (task.Tags.Contains(tag) && !task.IsDone)
            {
                list.Add(id, task);
            }
        }
        return list;
    }
}