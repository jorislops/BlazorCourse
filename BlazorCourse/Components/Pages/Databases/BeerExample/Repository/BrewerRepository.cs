using System.Data;
using BlazorCourse.Components.Pages.Databases.BierExample.Model;
using BlazorCourse.Components.Pages.Databases.BierExample.ViewModel;
using BlazorCourse.Services;
using MySqlConnector;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace BlazorCourse.Components.Pages.Databases.BierExample.Repository;

public class BrewerRepository
{
    private static string GetConnectionString()
    {
        var bierenConnectionString = ConfigurationHelper.Configuration.GetConnectionString("bieren");
        return bierenConnectionString!;
    }
    
    private static IDbConnection GetConnection()
    {
        return new MySqlConnection(GetConnectionString());
    }

    public static QueryFactory CreateQueryFactory()
    {
        var compiler = new MySqlCompiler();
        var db = new QueryFactory(GetConnection(), compiler);
        db.Logger = Console.WriteLine;
        return db;
    }
    
    
    public List<Brewer> Get()
    {
        return CreateQueryFactory()
            .Query("Brewer")
            .Select("BrewerId", "Name", "Country")
            .OrderBy("Name")
            .Get<Brewer>()
            .ToList();
    }

    public List<BrewerVm> GetBrouwersVm()
    {
        using var queryFactory = CreateQueryFactory();

        var countQuery = queryFactory.Query("Brewer as bb")
            .WhereColumns("bb.Name", "=", "br.Name").AsCount();
        
        return queryFactory
            .Query("Brewer as br")
            .Join("Beer as b", "br.BrewerId", "b.BrewerId")
            .SelectRaw("br.BrewerId AS BrewerCode")
            .Select("br.Name", "br.Country")
            .SelectRaw("COUNT(b.BeerId) AS NumberOfBeers")
            .Select(countQuery, "NumberOfBrewersWithSameName")
            // .SelectRaw("(SELECT COUNT(*) FROM Brewer bb WHERE bb.Name = br.Name) AS NumberOfBrewersWithSameName")
            .GroupBy("br.BrewerId", "br.Name", "br.Country")
            .OrderByDesc("NumberOfBeers")
            .Get<BrewerVm>()
            .ToList();
    }
}