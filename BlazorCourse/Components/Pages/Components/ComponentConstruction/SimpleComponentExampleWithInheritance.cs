using Microsoft.AspNetCore.Components;

namespace BlazorCourse.Components.Pages.Components.ComponentConstruction;

public class SimpleComponentExampleBase : ComponentBase 
{
    public string Message { get; set; } = "Hello from BaseComponent! " +DateTime.Now.ToShortDateString();
}