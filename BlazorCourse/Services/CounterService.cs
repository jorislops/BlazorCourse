namespace BlazorCourse.Services;

public class CounterService
{
    public int Counter { get; set; }

    public event Action? CountHasChanged;

    public void IncrementCounter()
    {
        Counter++;
        CountHasChanged?.Invoke();
    }
}