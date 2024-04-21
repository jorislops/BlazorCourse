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

    public List<Brouwer> Get()
    {
        var sql = "SELECT brouwcode, naam, land FROM brouwer ORDER BY naam";

        using var connection = new MySqlConnection(GetConnectionString());
        return connection.Query<Brouwer>(sql).ToList();
    }

    public List<BrouwerVm> GetBrouwersVm()
    {
        using var connection = new MySqlConnection(GetConnectionString());

        var sql =
            """
                    SELECT br.brouwcode, br.naam, br.land, COUNT(b.biercode) AS AantalBieren,
                       (SELECT COUNT(bb.naam) FROM brouwer bb WHERE bb.naam = br.naam)
                           AS AantalBrouwersZelfdeNaam
                FROM
                    brouwer as br
                        JOIN bier as b ON br.brouwcode = b.brouwcode
                GROUP BY br.brouwcode, br.naam, br.land
                ORDER BY AantalBieren DESC
            """;

        var brouwerVms = connection.Query<BrouwerVm>(sql).ToList();
        return brouwerVms;
    }
}