using BlazorCourse.Components.Pages.BierExample.Model;
using Dapper;
using MySqlConnector;

namespace BlazorCourse.Components.Pages.BierExample.Repository;

public class BrouwerRepository
{
    private string GetConnectionString()
    {
        return "Server=localhost;Database=bieren;Uid=root;Pwd=Test@1234!;";
    }
    
    public List<Brouwer> Get()
    {
        var sql = "SELECT brouwcode, naam, land FROM brouwer ORDER BY naam";
        
        using var connection = new MySqlConnection(GetConnectionString());
        return connection.Query<Brouwer>(sql).ToList();
    }
}