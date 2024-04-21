namespace BlazorCourse.Components.Pages.Components.ParentChildServiceExample;

public class MessageService
{
    public string Message { get; set; } = "...";
    public event Action? SaidSomething;

    public void SaySomething(string message)
    {
        Message = message;
        SaidSomething?.Invoke();
    }
}