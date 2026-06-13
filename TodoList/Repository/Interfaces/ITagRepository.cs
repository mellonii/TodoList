using TodoList.Models;

namespace TodoList.Repository.Interfaces;

internal interface ITagRepository
{
    void AddTags(Todo todo, string text);
    void DeleteTags(Todo todo, string text);
    int GetTagCount(Todo todo);
    string? FindByTag(string tag);
}