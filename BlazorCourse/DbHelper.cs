// using System.Data;
// using BlazorCourse.Services;
// using MySqlConnector;
// using SqlKata.Compilers;
// using SqlKata.Execution;
//
// namespace BlazorCourse;
//
// public class DbHelper
// {
//     private static string GetConnectionString()
//     {
//         var bierenConnectionString = ConfigurationHelper.Configuration.GetConnectionString("bieren");
//         // Console.WriteLine("ConnectionString bieren: " +bierenConnectionString);
//         return bierenConnectionString!;
//         // return "Server=localhost;Database=bieren;Uid=root;Pwd=Test@1234!;";
//     }
//
//     private static IDbConnection GetConnection()
//     {
//         return new MySqlConnection(GetConnectionString());
//     }
//
//     public static QueryFactory CreateQueryFactory()
//     {
//         var compiler = new MySqlCompiler();
//         var db = new QueryFactory(GetConnection(), compiler);
//         db.Logger = Console.WriteLine;
//         return db;
//     }
// }