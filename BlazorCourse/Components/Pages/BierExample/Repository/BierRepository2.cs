using BlazorCourse.Components.Pages.BierExample.Model;
using Dapper;
using MySqlConnector;

namespace BlazorCourse.Components.Pages.BierExample.Repository;

public class BierRepository2
{
    private string GetConnectionString()
    {
        return "Server=localhost;Database=bieren;Uid=root;Pwd=Test@1234!;";
    }

    public class PageResult
    {
        public List<Bier> Bieren { get; set; }
        public int Count { get; set; }
        
        public int PageSize { get; set; } = 10;
        
        public int CurrentPage { get; set; } = 1;
        
        public int TotalPages => (int)Math.Ceiling(Count / (double)PageSize);
        
        public bool HasPreviousPage => CurrentPage > 1;
        
        public bool HasNextPage => CurrentPage < TotalPages;
    }
    
    public PageResult GetBieren(
        int? brouwCode = null, string brouwnaam = null, string land = null,
        string orderBy = "naam", string dir = "ASC", int CurrentPage = 1, int PageSize = 10)
    {

        var builder = new SqlBuilder()
            .OrderBy($"{orderBy} {dir}");
        
        if(brouwCode != null)
            builder.Where("brouwcode = @brouwCode", new {brouwCode});
        if(!string.IsNullOrWhiteSpace(brouwnaam) && !string.IsNullOrWhiteSpace(land)) 
            builder.Where("brouwcode = (SELECT DISTINCT brouwcode FROM brouwer WHERE naam = @brouwnaam AND land = @land)", 
                new {brouwnaam, land});
        else if(!string.IsNullOrWhiteSpace(brouwnaam))
            builder.Where("brouwcode = (SELECT DISTINCT brouwcode FROM brouwer WHERE naam = @brouwnaam)", 
                new {brouwnaam});
        
        
        var sql = """
                    SELECT biercode, naam, type, stijl, alcohol, brouwcode
                    FROM bier
                        /**where**/
                        /**orderby**/
                    LIMIT @limit OFFSET @offset
                  """;

        var sqlCount = """
                       SELECT COUNT(*)
                       FROM bier
                           /**where**/
                       """;
        
        var selector = builder.AddTemplate(sql, new {limit = 10, offset = 0});
        var counter = builder.AddTemplate(sqlCount);
        
        
        using var connection = new MySqlConnection(GetConnectionString());
        var beers = 
            connection
            .Query<Bier>(selector.RawSql, selector.Parameters)
            .ToList();
        
        var numberOfBeers = connection.ExecuteScalar<int>(counter.RawSql, counter.Parameters);
        return new PageResult()
        {
            Bieren = beers,
            Count = numberOfBeers,
            PageSize = 10,
            CurrentPage = 1
        };
    }
    
    
}