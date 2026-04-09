using BlazorCourse.Models;
using BlazorCourse.Services;
using MySqlConnector;
using SqlKata.Compilers;
using SqlKata.Execution;

namespace BlazorCourse.Components.Pages.Databases.TodoExample.Repositories;

public class TodoRepository
{
    private readonly string _connectionString =
        ConfigurationHelper.Configuration.GetConnectionString("todo")!;

    private QueryFactory CreateQueryFactory()
    {
        return new QueryFactory(new MySqlConnection(_connectionString), new MySqlCompiler());
    }

    public List<TodoItem> Get()
    {
        using var queryFactory = CreateQueryFactory();

        return queryFactory
            .Query("TodoItem")
            .Select("Id", "ParentId", "Title", "Description", "IsDone", "CreatedAt", "CompletedAt")
            .Where("ParentId", null)
            .OrderBy("Id")
            .Get<TodoItem>()
            .ToList();
    }

    public List<TodoItem> GetByParentId(int parentId)
    {
        using var queryFactory = CreateQueryFactory();

        return queryFactory
            .Query("TodoItem")
            .Where("ParentId", parentId)
            .OrderBy("Id")
            .Get<TodoItem>()
            .ToList();
    }

    public TodoItem? GetById(int todoId)
    {
        using var queryFactory = CreateQueryFactory();

        return queryFactory
            .Query("TodoItem")
            .Where("Id", todoId)
            .FirstOrDefault<TodoItem>();
    }

    public TodoItem Add(TodoItem newTodoItem)
    {
        using var queryFactory = CreateQueryFactory();

        var todoId = queryFactory
            .Query("TodoItem")
            .InsertGetId<int>(newTodoItem);

        return GetById(todoId)!;
    }


    public void Remove(int todoItemId)
    {
        // Deleting a tree recursively is still more complex than using ON DELETE CASCADE,
        // but SQLKata keeps the queries strongly structured.
        using var queryFactory = CreateQueryFactory();

        var childrenIds = queryFactory
            .Query("TodoItem")
            .Select("Id")
            .Where("ParentId", todoItemId)
            .OrderBy("Id")
            .Get<int>()
            .ToList();


        foreach (var childrenId in childrenIds)
        {
            Remove(childrenId);
        }

        queryFactory
            .Query("TodoItem")
            .Where("Id", todoItemId)
            .Delete();
    }

    public void Update(TodoItem todoItem)
    {
        using var queryFactory = CreateQueryFactory();

        queryFactory
            .Query("TodoItem")
            .Where("Id", todoItem.Id)
            .Update(todoItem);
    }
}