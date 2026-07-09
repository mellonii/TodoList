using TodoServer.Models;

namespace TodoServer.Repository.Interfaces;

internal interface ITagRepository
{
    void AddTags(Todo todo, string text);
    void DeleteTags(Todo todo, string text);
    int GetTagCount(Todo todo);
    Dictionary<int, Todo> FindByTag(string tag);
}