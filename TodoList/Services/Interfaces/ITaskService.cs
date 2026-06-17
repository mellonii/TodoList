namespace TodoList.Services.Interfaces;

internal interface ITaskService
{
    void AddTask();
    void DoneTask();
    void DeleteTask();
    void ShowTasks();
    void GetStats();
    void SetPriority();
}