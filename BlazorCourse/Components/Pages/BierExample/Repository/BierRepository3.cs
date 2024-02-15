using BlazorCourse.Components.Pages.BierExample.Model;
using Dapper;
using MySqlConnector;

namespace BlazorCourse.Components.Pages.BierExample.Repository;

public class BierRepository3
{
    public PagedResult<Bier> Get(PageFilterSorting pageFilterSorting)
    {
        using var connection = new MySqlConnection(GetConnectionString());
        var builder = new SqlBuilder()
            .OrderBy($"{pageFilterSorting.OrderBy} {pageFilterSorting.Dir}");

        if (pageFilterSorting.BrouwCode != null)
            builder.Where("brouwcode = @BrouwCode", new { BrouwCode = pageFilterSorting.BrouwCode });
        if (!string.IsNullOrWhiteSpace(pageFilterSorting.Brouwnaam) &&
            !string.IsNullOrWhiteSpace(pageFilterSorting.Land))
            builder.Where(
                "brouwcode = (SELECT DISTINCT brouwcode FROM brouwer WHERE naam = @Brouwnaam AND land = @Land)",
                new { Brouwnaam = pageFilterSorting.Brouwnaam, Land = pageFilterSorting.Land });
        else if (!string.IsNullOrWhiteSpace(pageFilterSorting.Brouwnaam))
            builder.Where("brouwcode = (SELECT DISTINCT brouwcode FROM brouwer WHERE naam = @Brouwnaam)",
                new { Brouwnaam = pageFilterSorting.Brouwnaam });

        var sql = @"
                    SELECT biercode, naam, type, stijl, alcohol, brouwcode
                    FROM bier
                        /**where**/
                        /**orderby**/
                    LIMIT @limit OFFSET @offset
                  ";

        var sqlCount = @"
                       SELECT COUNT(*)
                       FROM bier
                           /**where**/
                       ";

        var selector = builder.AddTemplate(sql, new
        {
            limit = pageFilterSorting.PageSize,
            offset = (pageFilterSorting.CurrentPage - 1) * pageFilterSorting.PageSize
        });
        var counter = builder.AddTemplate(sqlCount);
        
        var items = connection.Query<Bier>(selector.RawSql, selector.Parameters).ToList();
        var count = connection.QueryFirst<int>(counter.RawSql);
        return new PagedResult<Bier>
        {
            Items = items,
            TotalItems = count,
            Page = pageFilterSorting.CurrentPage,
            PageSize = pageFilterSorting.PageSize
        };
    }

    private string GetConnectionString()
    {
        return "Server=localhost;Database=bieren;Uid=root;Pwd=Test@1234!;";
    }
}

public class PageFilterSorting
{
    public int? BrouwCode { get; set; }
    public string Brouwnaam { get; set; }
    public string Land { get; set; }
    public string OrderBy { get; set; } = "naam";
    public string Dir { get; set; } = "ASC";
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class PagedResult<T>
{
    public List<T> Items { get; set; }
    public int TotalItems { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}