namespace BlazorCourse.Models;

public class TodoItem
{
    public int Id { get; set; }
    
    public int? ParentId { get; set; }
    public required string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsDone { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    
    public List<TodoItem> Items { get; set; } = new List<TodoItem>();
}