using BlazorCourse.Components.Pages.Databases.BierExample.Model;
using Dapper;
using MySqlConnector;

namespace BlazorCourse.Components.Pages.Databases.BierExample.Repository;

public class BierRepository
{
    public PagedResult<Bier> Get(PageFilterSorting pageFilterSorting)
    {
        if (!string.IsNullOrWhiteSpace(pageFilterSorting.NaamFilter))
        {
            pageFilterSorting.NaamFilter = $"%{pageFilterSorting.NaamFilter}%";    
        }
        else
        {
            pageFilterSorting.NaamFilter = null;
        }
        
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
                        AND
                            (@NaamFilter IS NULL OR naam LIKE @NaamFilter)
                     ORDER BY {pageFilterSorting.OrderBy} {pageFilterSorting.Dir}
                     LIMIT @PageSize OFFSET @Offset
                  """;
        
        using var connection = new MySqlConnection(GetConnectionString());
        var bieren = 
            connection
                    .Query<Bier>(sql, pageFilterSorting)
                    .ToList();

        // Not the nicest way to get the total count, but it works!!
        // An sql query builder would be better, but that's out of scope for this course. 
        var bierCountSQL = sql.Remove(sql.IndexOf("LIMIT") - 1);
        var bierCount = connection.ExecuteScalar<int>(
            $"SELECT COUNT(1) FROM ({bierCountSQL}) as B", pageFilterSorting);
        
        return new PagedResult<Bier>
        {
            Items = bieren,
            TotalItems = bierCount,
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

    public Bier? GetByCode(int biercode)
    {
        using var connection = new MySqlConnection(GetConnectionString());
        var sql = """
                    SELECT biercode, naam, type, stijl, alcohol, brouwcode
                    FROM bier WHERE biercode = @Biercode
                  """;
        return connection.QueryFirstOrDefault<Bier>(sql, new { Biercode = biercode });
    }

    public void Delete(int bierCode)
    {
        using var connection = new MySqlConnection(GetConnectionString());
        var sql = "DELETE FROM bier WHERE biercode = @Biercode";
        connection.Execute(sql, new { Biercode = bierCode });
    }
}