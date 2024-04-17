namespace BlazorCourse;

public class CodeViewerService
{
    public event Action? OnMenuChange;

    public string SelectedMenu { get; set; } = string.Empty;

    public void SelectedMenuMessage(string selectedMenu)
    {
        SelectedMenu = selectedMenu;
        OnMenuChange?.Invoke();
    }
}