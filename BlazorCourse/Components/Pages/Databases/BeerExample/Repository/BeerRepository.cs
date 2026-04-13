using System.Data;
using AutoMapper;
using BlazorCourse.Components.Pages.Databases.BierExample.Model;
using BlazorCourse.Services;
using MySqlConnector;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace BlazorCourse.Components.Pages.Databases.BierExample.Repository;

public class BeerRepository
{
    private static string GetConnectionString()
    {
        var bierenConnectionString = ConfigurationHelper.Configuration.GetConnectionString("bieren");
        // Console.WriteLine("ConnectionString bieren: " +bierenConnectionString);
        return bierenConnectionString!;
        // return "Server=localhost;Database=bieren;Uid=root;Pwd=Test@1234!;";
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
    
    public PagedResult<Beer> Get(PageFilterSorting pageFilterSorting, int pageIndex, int pageSize)
    {
        var beerNameFilter = string.IsNullOrWhiteSpace(pageFilterSorting.BeerName)
            ? null
            : $"%{pageFilterSorting.BeerName}%";

        using var queryFactory = CreateQueryFactory();

        // To prevent SQL injection, only allow sorting on known columns.
        // var allowedColumns = new[] { "BeerId", "Name", "Type", "Style", "Alcohol", "BrewerId" };
        // var orderBy = allowedColumns.Contains(pageFilterSorting.OrderBy) ? pageFilterSorting.OrderBy : "Name";
        

        var filteredQuery = queryFactory.Query("Beer")
            .Select("BeerId", "Name", "Type", "Style", "Alcohol", "BrewerId");

        if (pageFilterSorting.BrewerId.HasValue)
            filteredQuery.Where("BrewerId", pageFilterSorting.BrewerId.Value);

        if (!string.IsNullOrWhiteSpace(pageFilterSorting.BrewerName))
        {
            filteredQuery.WhereIn("BrewerId", brewerQuery =>
            {
                brewerQuery.From("Brewer")
                    .Select("BrewerId")
                    .Where("Name", pageFilterSorting.BrewerName);

                if (!string.IsNullOrWhiteSpace(pageFilterSorting.Country))
                    brewerQuery.Where("Country", pageFilterSorting.Country);

                return brewerQuery;
            });
        }

        if (!string.IsNullOrWhiteSpace(beerNameFilter))
            filteredQuery.WhereLike("Name", beerNameFilter);

        var bierCount = filteredQuery.Clone().Count<int>();

        var orderedQuery = pageFilterSorting.OrderDirection.ToUpper() switch
        {
            "DESC" => filteredQuery.OrderByDesc(pageFilterSorting.OrderBy),
            "ASC" => filteredQuery.OrderBy(pageFilterSorting.OrderBy),
            _ => filteredQuery
        };
        
        var brewerQuery = queryFactory
            .Query("Brewer");
        
        //Normally we can use Paginate<Beer>(), this is not possible with nested objects 
        var bierenDynamic = orderedQuery
            .Include("Brewer",  brewerQuery, "BrewerId", "BrewerId")
            .Limit(pageSize)
            .Offset((pageIndex - 1) * pageSize)// -1 to fix the offset
            .Get()
            .ToList();

        //trick to convert to nested objects with the help of AutoMapper
        var configuration = new MapperConfiguration(cfg => { }, new LoggerFactory());
        var mapper = configuration.CreateMapper();
        var bierenAsList = mapper.Map<List<Beer>>(bierenDynamic);
        
        return new PagedResult<Beer>
        {
            Items = bierenAsList,
            TotalItems = bierCount,
            Page = pageIndex,
            PageSize = pageSize
        };
    }

    public List<Beer> GetIncludeBrouwer()
    {
        using var queryFactory = CreateQueryFactory();

        var rows = queryFactory
            .Query("Beer as b")
            .Join("Brewer as br", "b.BrewerId", "br.BrewerId")
            .Select(
                "b.BeerId",
                "b.Name",
                "b.Type",
                "b.Style",
                "b.Alcohol",
                "b.BrewerId",
                "br.BrewerId as Brewer_BrewerId",
                "br.Name as Brewer_Name",
                "br.Country as Brewer_Country")
            .OrderBy("br.Name")
            .OrderBy("b.Name")
            .Get<BeerWithBrewerRow>()
            .ToList();

        return rows.Select(row => new Beer
        {
            BeerId = row.BeerId,
            Name = row.Name,
            Type = row.Type,
            Style = row.Style,
            Alcohol = row.Alcohol,
            BrewerId = row.BrewerId,
            Brewer = new Brewer
            {
                BrewerId = row.Brewer_BrewerId,
                Name = row.Brewer_Name,
                Country = row.Brewer_Country
            }
        }).ToList();
    }

    private sealed class BeerWithBrewerRow
    {
        public int BeerId { get; init; }
        public string Name { get; init; } = null!;
        public string Type { get; init; } = null!;
        public string Style { get; init; } = null!;
        public double? Alcohol { get; init; }
        public int? BrewerId { get; init; }
        public int Brewer_BrewerId { get; init; }
        public string Brewer_Name { get; init; } = null!;
        public string Brewer_Country { get; init; } = null!;
    }



    public void Add(Beer beer)
    {
        CreateQueryFactory()
            .Query("Beer")
            .Insert(beer);
    }

    public Beer? GetByCode(int beerId)
    {
        return CreateQueryFactory()
            .Query("Beer")
            .Where("BeerId", beerId)
            .FirstOrDefault<Beer>();
    }

    public void Delete(int beerId)
    {
        CreateQueryFactory()
            .Query("Beer")
            .Where("BeerId", beerId)
            .Delete();
    }
}