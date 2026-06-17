using TodoList.Models;

namespace TodoList.Repository;

public static class StaticData
{
    public static readonly Dictionary<int, Todo> TaskList = new();
}