using Microsoft.AspNetCore.Components;

namespace BlazorCourse.Components.Pages.ComponentsBasics;

public class BaseComponent : ComponentBase
{
    protected string Message { get; set; } = "Hello from BaseComponent! " +DateTime.Now.ToShortDateString();
}