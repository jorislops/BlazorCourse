using BlazorCourse.Models;
using Dapper;
using MySqlConnector;

namespace BlazorCourse.Repositories;

public class TodoRepository
{
    private readonly string _connectionString = "Server=localhost;Database=TodoBlazorCourse;Uid=root;Pwd=Test@1234!;";
    public List<TodoItem> Get()
    {
        using var connection = new MySqlConnection(_connectionString);

        string sql =
            """
                SELECT Id, ParentId, Title, Description, IsDone, CreatedAt, CompletedAt 
                FROM TodoItem 
                WHERE ParentId IS NULL -- this select only the top level items
                ORDER BY Id -- maybe we should order by CreatedAt
            """;
        var todos = connection.Query<TodoItem>(sql).ToList();
        return todos;
    }
    
    public List<TodoItem> GetByParentId(int itemId)
    {
        string sql =
            """
                SELECT Id, ParentId, Title, Description, IsDone, CreatedAt, CompletedAt
                FROM TodoItem
                WHERE ParentId = @ParentId
                ORDER BY Id
            """;
        using var connection = new MySqlConnection(_connectionString);
        var todos = connection.Query<TodoItem>(sql, new {ParentId = itemId}).ToList();
        return todos;
    }

    public TodoItem? GetById(int todoId)
    {
        string sql =
            """
                SELECT Id, ParentId, Title, Description, IsDone, CreatedAt, CompletedAt
                FROM TodoItem
                WHERE Id = @Id
            """;
        using var connection = new MySqlConnection(_connectionString);
        var todo = connection.QueryFirstOrDefault<TodoItem>(sql, new {Id = todoId});
        return todo;
    }

    public TodoItem Add(TodoItem newTodoItem)
    {
        string sql =
            """
                INSERT INTO TodoItem (ParentId, Title, Description, IsDone, CreatedAt, CompletedAt)
                VALUES (@ParentId, @Title, @Description, @IsDone, @CreatedAt, @CompletedAt);
                SELECT LAST_INSERT_ID();
            """;
        using var connection = new MySqlConnection(_connectionString);
        int newId = connection.ExecuteScalar<int>(sql, newTodoItem);
        return GetById(newId)!;
    }


    public void Remove(int todoItemId)
    {
        string sql =
            """
                -- we need to delete all the children items first
                -- or use a cascade delete
                DELETE FROM TodoItem
                WHERE ParentId = @Id;            

                DELETE FROM TodoItem
                WHERE Id = @Id
            """;
        using var connection = new MySqlConnection(_connectionString);
        int numEffectedRows = connection.Execute(sql, new {Id = todoItemId});
    }
}