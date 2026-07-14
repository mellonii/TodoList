using System.Text;
using TodoServer.Services;
using TodoServer.Exceptions;
using TodoServer.Repository;

namespace TodoServer;

class Program
{
    private static void HandleErrors(Action func)
    {
        try
        {
            func();
        }
        catch (BusinesException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
    
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        
        var taskRepository = new TaskRepository();
        var tagRepository = new TagRepository();
        var tagService = new TagService(tagRepository, taskRepository);
        var taskService = new TaskService(taskRepository);
        
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddOpenApi();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        app.UseHttpsRedirection();

        app.MapGet("/", () => "Hello World!");

        app.MapGet("/tasks", taskService.GetTasks);
        app.MapGet("/tasks/{id:int}", taskService.GetTaskById);
        app.MapGet("/tasks/{id:int}/tags", tagService.GetTagsById);
        app.MapGet("/tasks/{id:int}/priority", taskService.GetPriorityById);
        app.MapGet("/tasks/current", taskService.GetCurrentTasks);
        app.MapGet("/tasks/done", taskService.GetDoneTasks);

        app.MapGet("/tag", tagService.GetTags);
        app.MapGet("/tag/{tag}", tagService.FindByTag);
        
        app.MapGet("/add/task/{title}", taskService.AddSimpleTask);
        app.MapGet("/add/task/{title}/{time:DateTime}", taskService.AddDeadlinedTask);
        app.MapGet("/add/task/{title}/{text}", taskService.AddChecklistTask);
        app.MapGet("/add/tag/{id:int}/{tags}", tagService.AddTags);
        app.MapGet("/add/priority/{id:int}/{priority:int}", taskService.SetPriority);
        
        app.MapGet("/stats", taskService.GetStats);
        app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
            string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
        
        app.MapGet("/done/{id:int}", taskService.DoneTask);
        app.MapGet("/delete/{id:int}", taskService.DeleteTask);
        app.MapGet("/delete/tag/{id:int}/{tags}", tagService.DeleteTags);

        app.Run();
    }
}