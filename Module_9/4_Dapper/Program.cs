// https://github.com/DapperLib/Dapper
// https://www.nuget.org/packages/Dapper/

using System.Data;
using _4_Dapper;
using Dapper;
using Microsoft.Data.SqlClient;

const string ConnectionString = "Server=tcp:learn-m9-db-server.database.windows.net,1433;Initial Catalog=learn-m9-db;Persist Security Info=False;User ID=sk;Password=December2022#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

//ExecuteQuery();
//ExecuteStoredProcedure();

static void ExecuteQuery()
{
    // We want to find all employees from London
    var cityToQuery = "London";

    using var connection = new SqlConnection(ConnectionString);

    var query = "select EmployeeID, LastName, FirstName, Title, BirthDate, City " +
        "from Employees " +
        "where City = @city";

    var parameters = new { city = cityToQuery };

    var employees = connection.Query<Employee>(query, parameters);

    foreach (var employee in employees)
    {
        Console.Write($"Employee ID: {employee.EmployeeID} | ");
        Console.Write($"Last Name: {employee.LastName} | ");
        Console.Write($"First Name: {employee.FirstName} | ");
        Console.Write($"Title: {employee.Title}");
        Console.WriteLine();
    }
}

static void ExecuteStoredProcedure()
{
    using var connection = new SqlConnection(ConnectionString);

    var procedure = "[CustOrderHist]";

    var values = new { CustomerID = "QUICK" };

    var results = connection.Query(procedure, values, commandType: CommandType.StoredProcedure).ToList();

    results.ForEach(r => Console.WriteLine($"{r.ProductName} {r.Total}"));
}