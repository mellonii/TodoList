namespace TodoServer.Services.Interfaces;

internal interface ITaskService
{
    void AddSimpleTask(string title);
    void AddDeadlinedTask(string title, DateTimeOffset time);
    void AddChecklistTask(string title, string text);
    void DoneTask(int id);
    void DeleteTask(int id);
    string GetTasks();
    string GetCurrentTasks();
    string GetDoneTasks();
    string GetStats();
    void SetPriority(int id, int priority);
    string GetTaskById(int id);
    string GetPriorityById(int id);
}