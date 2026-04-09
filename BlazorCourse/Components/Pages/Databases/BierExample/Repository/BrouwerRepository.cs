using BlazorCourse.Components.Pages.Databases.BierExample.Model;
using BlazorCourse.Components.Pages.Databases.BierExample.ViewModel;
using Radzen;
using SqlKata.Execution;

namespace BlazorCourse.Components.Pages.Databases.BierExample.Repository;

public class BrouwerRepository
{
    public List<Brewer> Get()
    {
        return DbHelper.CreateQueryFactory()
            .Query("Brewer")
            .Select("BrewerId", "Name", "Country")
            .OrderBy("Name")
            .Get<Brewer>()
            .ToList();
    }

    public List<BrewerVm> GetBrouwersVm()
    {
        using var queryFactory = DbHelper.CreateQueryFactory();

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