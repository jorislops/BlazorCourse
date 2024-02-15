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
                        CASE WHEN @orderBy = 'naam' and @dir = 'desc' THEN naam END DESC,
                        CASE WHEN @orderBy = 'naam' THEN naam END ASC,
                        
                        CASE WHEN @orderBy = 'stijl' and @dir = 'desc' THEN stijl END DESC,
                        CASE WHEN @orderBy = 'stijl' THEN stijl END ASC,
                        
                        CASE WHEN @orderBy = 'alcohol' and @dir = 'desc' THEN alcohol END DESC,
                        CASE WHEN @orderBy = 'alcohol' THEN alcohol END ASC
                  
                  
                  """;
        
        
        
        using var connection = new MySqlConnection(GetConnectionString());
        var beers = 
            connection
            .Query<Bier>(sql, new {brouwCode, brouwnaam, land, orderBy, dir})
            .ToList();
        return beers;
    }
    
    
}