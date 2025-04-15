using BlazorCourse.Components.Pages.Databases.BierExample.Model;
using BlazorCourse.Services;
using Dapper;
using MySqlConnector;

namespace BlazorCourse.Components.Pages.Databases.BierExample.Repository;

public class BierRepository
{
    public PagedResult<Beer> Get(PageFilterSorting pageFilterSorting)
    {
        if (!string.IsNullOrWhiteSpace(pageFilterSorting.BeerName))
            pageFilterSorting.BeerName = $"%{pageFilterSorting.BeerName}%";
        else
            pageFilterSorting.BeerName = null;

        //To prevent SQL Injection, we only allow certain columns to be sorted on
        var allowedColumns = new[] { "BeerId", "Name", "Type", "Style", "Alcohol", "BrewerId" };
        if (!allowedColumns.Contains(pageFilterSorting.OrderBy)) pageFilterSorting.OrderBy = "Name";
        if (!pageFilterSorting.Dir.Equals("ASC", StringComparison.OrdinalIgnoreCase) 
            && !pageFilterSorting.Dir.Equals("DESC", StringComparison.OrdinalIgnoreCase)) pageFilterSorting.Dir = "ASC";

        var sql = $"""
                     SELECT BeerId, Name, Type, Style, Alcohol, BrewerId
                     FROM Beer
                     WHERE
                         (@BrewerId IS NULL OR BrewerId = @BrewerId)
                         AND
                             (@BrewerName IS NULL OR
                             BrewerId = (SELECT DISTINCT BrewerId FROM Brewer
                                 WHERE Name = @BrewerName
                                     AND (@Country IS NULL OR Country = @Country)))
                         AND
                             (@BeerName IS NULL OR Name LIKE @BeerName)
                     -- Be aware of SQL Injection, but this is a simple example!!!!
                     -- To solve this problem, check the valid values for the parameters
                     
                      ORDER BY {pageFilterSorting.OrderBy} {pageFilterSorting.Dir}
                      LIMIT @PageSize OFFSET @Offset
                   """;

        using var connection = new MySqlConnection(GetConnectionString());
        var bieren = connection
            .Query<Beer>(sql, pageFilterSorting)
            .ToList();

        // Not the nicest way to get the total count, but it works!!
        // An sql query builder would be better, but that's out of scope for this course. 
        var bierCountSQL = sql.Remove(sql.IndexOf("LIMIT") - 1);
        var bierCount = connection.ExecuteScalar<int>(
            $"SELECT COUNT(1) FROM ({bierCountSQL}) as B", pageFilterSorting);

        return new PagedResult<Beer>
        {
            Items = bieren,
            TotalItems = bierCount,
            Page = pageFilterSorting.CurrentPage,
            PageSize = pageFilterSorting.PageSize
        };
    }

    public List<Beer> GetIncludeBrouwer()
    {
        var sql = """
                     SELECT b.BeerId, b.Name, b.Type, b.Style, b.Alcohol, b.BrewerId, 
                            '' as brewerSplit, 
                            br.Name, br.Country
                     FROM Beer b
                         JOIN Brewer br ON b.BrewerId = br.BrewerId
                     ORDER BY br.Name, b.Name
                  """;

        var connection = new MySqlConnection(GetConnectionString());
        return connection.Query<Beer, Brewer, Beer>(sql, (bier, brouwer) =>
        {
            bier.Brewer = brouwer;
            return bier;
        }, splitOn: "brewerSplit").ToList();
    }

    private string GetConnectionString()
    {
        var bierenConnectionString = ConfigurationHelper.Configuration.GetConnectionString("bieren");
        // Console.WriteLine("ConnectionString bieren: " +bierenConnectionString);
        return bierenConnectionString!;
        // return "Server=localhost;Database=bieren;Uid=root;Pwd=Test@1234!;";
    }

    public void Add(Beer beer)
    {
        using var connection = new MySqlConnection(GetConnectionString());
        var sql = "INSERT INTO Beer (Name, Type, Style, Alcohol, BrewerId) " +
                  "VALUES (@Name, @Type, @Style, @Alcohol, @BrewerId)";
        connection.Execute(sql, beer);
    }

    public Beer? GetByCode(int beerId)
    {
        using var connection = new MySqlConnection(GetConnectionString());
        var sql = """
                    SELECT BeerId, Name, Type, Style, Alcohol, BrewerId
                    FROM Beer WHERE BeerId = @BeerId
                  """;
        return connection.QueryFirstOrDefault<Beer>(sql, new { BeerId = beerId });
    }

    public void Delete(int beerId)
    {
        using var connection = new MySqlConnection(GetConnectionString());
        var sql = "DELETE FROM Beer WHERE BeerId = @beerId";
        connection.Execute(sql, new { BeerId = beerId });
    }
}