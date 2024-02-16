namespace BlazorCourse.Components.Pages.BierExample.Repository;

public class PageFilterSorting
{
    public int? BrouwCode { get; set; }
    public string Brouwnaam { get; set; }
    public string Land { get; set; }
    public string OrderBy { get; set; } = "naam";
    public string Dir { get; set; } = "ASC";
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    
    public int Offset => (CurrentPage - 1) * PageSize;
}