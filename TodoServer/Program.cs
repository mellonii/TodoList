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
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.Clear();
        
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

        var taskGroup = app.MapGroup("/tasks");
        taskGroup.MapGet("/", GetTasks);
        taskGroup.MapGet("/{id:int}", GetTaskById);
        taskGroup.MapGet("/{id:int}/tags", GetTagsById);
        taskGroup.MapGet("/{id:int}/priority", GetPriorityById);
        taskGroup.MapGet("/current", GetCurrentTasks);
        taskGroup.MapGet("/done", GetDoneTasks);

        var tagGroup = app.MapGroup("/tags");
        tagGroup.MapGet("/", GetTags);
        tagGroup.MapGet("/{tag}", GetTasksByTag);
        
        app.MapGet("/stats", GetStats);

        app.MapGet("/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
            string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
        
        
        static string GetTasks() => "GetTasks";
        static string GetTaskById(int id) => $"GetTaskById {id}";
        static string GetTagsById(int id) => $"GetTagsById {id}";
        static string GetPriorityById(int id) => $"GetPriorityById {id}";
        static string GetCurrentTasks() => "GetCurrentTasks";
        static string GetDoneTasks() => "GetDoneTasks";
        static string GetTags() => "GetTags";
        static string GetTasksByTag(string tag) => $"GetTasksByTag {tag}"; // tagService.FindByTag();
        static string GetStats() => $"GetStats"; // taskService.GetStats();

        app.Run();
        
        /*
        HandleErrors(taskService.AddTask); 
        HandleErrors(taskService.DoneTask);
        HandleErrors(taskService.DeleteTask);
        HandleErrors(tagService.AddTags);
        HandleErrors(tagService.DeleteTags);
        HandleErrors(taskService.SetPriority);
        */
    }
}