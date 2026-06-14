using TodoList.Models;

namespace TodoList.Repository.Interfaces;

internal interface ITaskRepository
{
    void Add<T>(T task) where T : Todo;
    void DoneCurrentTask(int id);
    void DeleteDoneTask(int id);
    string GetCurrentTasksList();
    string GetDoneTasksList();
    (int, int, int) GetStats();
    bool SetPriority(Todo task, int priority);
}