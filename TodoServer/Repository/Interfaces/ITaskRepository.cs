using TodoServer.Models;

namespace TodoServer.Repository.Interfaces;

internal interface ITaskRepository
{
    void Add<T>(T task) where T : Todo;
    Todo? GetByIdOrDefault(int id);
    int GetCurrentTasksCount();
    int GetDoneTasksCount();
    bool EnsureTaskNotEmpty();
    void DoneCurrentTask(int id);
    string DeleteDoneTask(int id);
    Dictionary<int, Todo> GetCurrentTasksList();
    Dictionary<int, Todo> GetDoneTasksList();
    TodoStats GetStats();
    bool SetPriority(Todo task, int priority);
}