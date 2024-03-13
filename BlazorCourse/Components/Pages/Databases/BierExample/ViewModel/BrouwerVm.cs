namespace BlazorCourse.Components.Pages.Databases.BierExample.ViewModel;

public class BrouwerVm
{
    public int Brouwcode { get; set; }
    public string Naam { get; set; } = null!;
    public string Land { get; set; } = null!;
    public int AantalBieren { get; set; }
    public int AantalBrouwersZelfdeNaam { get; set; }
}