using BlazorCourse.Components.Pages.BierExample.Model;
using Dapper;
using MySqlConnector;

namespace BlazorCourse.Components.Pages.BierExample.Repository;

public class BierRepository
{
    private string GetConnectionString()
    {
        return "Server=localhost;Database=bieren;Uid=root;Pwd=Test@1234!;";
    }
    
    public List<Bier> GetBieren(
        int? brouwCode = null, string brouwnaam = null, string land = null,
        string orderBy = "naam", string dir = "ASC")
    {
        var sql = """
                    SELECT biercode, naam, type, stijl, alcohol, brouwcode
                    FROM bier
                    WHERE 
                        (@brouwCode IS NULL OR brouwcode = @brouwCode)
                        AND 
                            (@brouwnaam IS NULL OR
                            brouwcode = (SELECT DISTINCT brouwcode FROM brouwer 
                                WHERE naam = @brouwnaam 
                                    AND (@land IS NULL OR land = @land)))
                    ORDER BY 
                  """;
        
        sql += $"{orderBy} {dir}";
        
        
        using var connection = new MySqlConnection(GetConnectionString());
        var beers = 
            connection
            .Query<Bier>(sql, new {brouwCode, brouwnaam, land, orderBy, dir})
            .ToList();
        return beers;
    }
    
    
}