using Microsoft.AspNetCore.Components;

namespace BlazorCourse.Components.Pages.Components.ComponentConstruction;

public partial class SimpleComponentPartialExample : ComponentBase
{
    protected string Message { get; set; } = "Hello from partial Page! " + DateTime.Now.ToShortDateString();
}