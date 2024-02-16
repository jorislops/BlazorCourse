using BlazorCourse.Components.Pages.BierExample.Model;
using Dapper;
using Dapper.SimpleSqlBuilder;
using Dapper.SimpleSqlBuilder.Extensions;
using Dapper.SimpleSqlBuilder.FluentBuilder;
using MySqlConnector;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace BlazorCourse.Components.Pages.BierExample.Repository;

public class BierRepository4
{
    public BierRepository4()
    {
    }

    public static int COUNT = 1;
    
    public PagedResult<Bier> Get(PageFilterSorting pageFilterSorting)
    {
        using var connection = new MySqlConnection(GetConnectionString());
        
        var sql = $"""
                    SELECT biercode, naam, type, stijl, alcohol, brouwcode
                    FROM bier
                    WHERE
                        (@BrouwCode IS NULL OR brouwcode = @brouwCode)
                        AND
                            (@Brouwnaam IS NULL OR
                            brouwcode = (SELECT DISTINCT brouwcode FROM brouwer
                                WHERE naam = @Brouwnaam
                                    AND (@Land IS NULL OR land = @Land)))
                     ORDER BY {pageFilterSorting.OrderBy} {pageFilterSorting.Dir}
                     LIMIT @PageSize OFFSET @Offset
                  """;

        var bieren = connection.Query<Bier>(sql, pageFilterSorting)
            .ToList();
        

        
        return new PagedResult<Bier>
        {
            Items = bieren,
            TotalItems = 1000,
            Page = pageFilterSorting.CurrentPage,
            PageSize = pageFilterSorting.PageSize
        };
    }

    private string GetConnectionString()
    {
        return "Server=localhost;Database=bieren;Uid=root;Pwd=Test@1234!;";
    }

    public void Add(Bier bier)
    {
        using var connection = new MySqlConnection(GetConnectionString());
        var sql = "INSERT INTO bier (naam, type, stijl, alcohol, brouwcode) " +
                  "VALUES (@Naam, @Type, @Stijl, @Alcohol, @Brouwcode)";
        connection.Execute(sql, bier);
    }
}