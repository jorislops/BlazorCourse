using BlazorCourse.Components.Pages.Databases.BierExample.Model;

namespace BlazorCourse.Components.Pages.Databases.BierExample.Repository;

public class PageFilterSorting
{
    public int? BrewerId { get; set; }
    public string BrewerName { get; set; } = null!;
    public string? BeerName { get; set; }
    public string Country { get; set; } = null!;
    
    
    
    public string OrderBy { get; set; } = nameof(Beer.Name);
    public string Dir { get; set; } = "ASC";
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public int Offset => (CurrentPage - 1) * PageSize;
    
}