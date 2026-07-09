using TodoServer.Models;

namespace TodoServer.Repository;

public static class StaticData
{
    public static readonly Dictionary<int, Todo> TaskList = new();
}