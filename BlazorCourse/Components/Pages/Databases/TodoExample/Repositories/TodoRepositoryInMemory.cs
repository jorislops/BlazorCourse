using BlazorCourse.Models;

namespace BlazorCourse.Components.Pages.Databases.Repositories;

public class TodoRepositoryInMemory
{
    public List<TodoItem> Get()
    {
        var todos = new List<TodoItem>
        {
            new()
            {
                Id = 1, Title = "Do this", IsDone = false, CreatedAt = DateTime.Now, Items = new List<TodoItem>
                {
                    new() { Id = 4, Title = "Do that", IsDone = true, CreatedAt = DateTime.Now.AddDays(-1) },
                    new() { Id = 5, Title = "Do something else", IsDone = false, CreatedAt = DateTime.Now.AddDays(-2) }
                }
            },
            new()
            {
                Id = 2, Title = "Do that", IsDone = false, CreatedAt = DateTime.Now.AddDays(-1),
                Items = new List<TodoItem>
                {
                    new() { Id = 6, Title = "Do this", IsDone = false, CreatedAt = DateTime.Now },
                    new() { Id = 7, Title = "Do something else", IsDone = false, CreatedAt = DateTime.Now.AddDays(-2) }
                }
            },
            new() { Id = 3, Title = "Do something else", IsDone = true, CreatedAt = DateTime.Now.AddDays(-2) }
        };
        return todos;
    }

    public TodoItem? GetById(int todoId)
    {
        return Get().FirstOrDefault(t => t.Id == todoId);
    }
}