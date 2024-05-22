using BlazorCourse.Models;
using BlazorCourse.Services;
using Dapper;
using MySqlConnector;

namespace BlazorCourse.Components.Pages.Databases.Repositories;

public class TodoRepository
{
    private readonly string _connectionString =
        ConfigurationHelper.Configuration.GetConnectionString("todo")!;

    public List<TodoItem> Get()
    {
        using var connection = new MySqlConnection(_connectionString);

        var sql =
            """
                SELECT Id, ParentId, Title, Description, IsDone, CreatedAt, CompletedAt
                FROM TodoItem
                WHERE ParentId IS NULL -- this select only the top level items
                ORDER BY Id -- maybe we should order by CreatedAt
            """;
        var todos = connection.Query<TodoItem>(sql).ToList();
        return todos;
    }

    public List<TodoItem> GetByParentId(int parentId)
    {
        var sql =
            """
                SELECT Id, ParentId, Title, Description, IsDone, CreatedAt, CompletedAt
                FROM TodoItem
                WHERE ParentId = @ParentId
                ORDER BY Id
            """;
        using var connection = new MySqlConnection(_connectionString);
        var todos = connection.Query<TodoItem>(sql, new { ParentId = parentId }).ToList();
        return todos;
    }

    public TodoItem? GetById(int todoId)
    {
        var sql =
            """
                SELECT Id, ParentId, Title, Description, IsDone, CreatedAt, CompletedAt
                FROM TodoItem
                WHERE Id = @Id
            """;
        using var connection = new MySqlConnection(_connectionString);
        var todo = connection.QueryFirstOrDefault<TodoItem>(sql, new { Id = todoId });
        return todo;
    }

    public TodoItem Add(TodoItem newTodoItem)
    {
        var sql =
            """
                INSERT INTO TodoItem (ParentId, Title, Description, IsDone, CreatedAt, CompletedAt)
                VALUES (@ParentId, @Title, @Description, @IsDone, @CreatedAt, @CompletedAt);
                SELECT LAST_INSERT_ID();
            """;
        using var connection = new MySqlConnection(_connectionString);
        var newId = connection.ExecuteScalar<int>(sql, newTodoItem);
        return GetById(newId)!;
    }


    public void Remove(int todoItemId)
    {
        //Note: I don't like this method and didn't test it, so it may not work 
        
        //This method becomes complex because we have to delete all the children items first
        //I think is better to use a cascade delete (https://www.mysqltutorial.org/mysql-basics/mysql-on-delete-cascade/)
        var getChildrenSql =
            """
                SELECT Id
                FROM TodoItem
                WHERE ParentId = @Id
            """;
        
        using var connection = new MySqlConnection(_connectionString);
        var childrenIds = connection.Query<int>(getChildrenSql, new { Id = todoItemId }).ToList();
        foreach (var childrenId in childrenIds)
        {
            Remove(childrenId);
        }
        
        var sql =
            """
                -- we need to delete all the children items first
                -- or use a cascade delete
                DELETE FROM TodoItem
                WHERE ParentId = @Id;
            
                DELETE FROM TodoItem
                WHERE Id = @Id
            """;
        // using var connection = new MySqlConnection(_connectionString);
        var numEffectedRows = connection.Execute(sql, new { Id = todoItemId });
    }

    public void Update(TodoItem todoItem)
    {
        var sql =
            """
                UPDATE TodoItem
                SET Title = @Title,
                    Description = @Description,
                    IsDone = @IsDone,
                    CompletedAt = @CompletedAt
                WHERE Id = @Id
            """;
        using var connection = new MySqlConnection(_connectionString);
        var numEffectedRows = connection.Execute(sql, todoItem);
    }
}