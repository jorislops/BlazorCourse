namespace BlazorCourse.Components.Pages.Components.ParentChildServiceExample;

//https://www.youtube.com/watch?v=QSM1Vhu1Nsc

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