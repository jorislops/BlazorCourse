namespace BlazorCourse;

public class AppState
{
    public int Counter { get; set; } = 0;
    
    public event Action? CountHasChanged;
    public void IncrementCounter()
    {
        Counter++;
        CountHasChanged?.Invoke();
    }
}