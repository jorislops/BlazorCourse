using BlazorCourse.Components.Pages.Databases.BierExample.Model;
using BlazorCourse.Components.Pages.Databases.BierExample.ViewModel;
using BlazorCourse.Services;
using Dapper;
using MySqlConnector;

namespace BlazorCourse.Components.Pages.Databases.BierExample.Repository;

public class BrouwerRepository
{
    private string GetConnectionString()
    {
        return ConfigurationHelper.Configuration.GetConnectionString("Bieren")!;
    }

    public List<Brewer> Get()
    {
        var sql = "SELECT BrewerId, Name, Country FROM Brewer ORDER BY Name";

        using var connection = new MySqlConnection(GetConnectionString());
        return connection.Query<Brewer>(sql).ToList();
    }

    public List<BrewerVm> GetBrouwersVm()
    {
        using var connection = new MySqlConnection(GetConnectionString());

        var sql =
            """
                    SELECT br.BrewerId, br.Name, br.Country, COUNT(b.BeerId) AS NumberOfBeers,
                       (SELECT COUNT(bb.Name) FROM Brewer bb WHERE bb.Name = br.Name)
                           AS NumberOfBrewersWithSameName
                FROM
                    Brewer as br
                        JOIN Beer as b ON br.BrewerId = b.BrewerId
                GROUP BY br.BrewerId, br.Name, br.Country
                ORDER BY NumberOfBeers DESC
            """;

        var brouwerVms = connection.Query<BrewerVm>(sql).ToList();
        return brouwerVms;
    }
}