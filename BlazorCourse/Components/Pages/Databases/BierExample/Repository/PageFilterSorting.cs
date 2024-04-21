namespace BlazorCourse.Components.Pages.Databases.BierExample.Repository;

public class PageFilterSorting
{
    public int? BrouwCode { get; set; }
    public string Brouwnaam { get; set; } = null!;
    public string Land { get; set; } = null!;
    public string OrderBy { get; set; } = "naam";
    public string Dir { get; set; } = "asc";
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public int Offset => (CurrentPage - 1) * PageSize;
    public string? NaamFilter { get; set; }
}