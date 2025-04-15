namespace BlazorCourse.Components.Pages.Databases.BierExample.ViewModel;

public class BrewerVm
{
    public int BrewerCode { get; set; }
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public int NumberOfBeers { get; set; }
    public int NumberOfBrewersWithSameName { get; set; }
}