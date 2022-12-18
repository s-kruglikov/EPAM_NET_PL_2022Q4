// ADO.NET Architecture https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-architecture
// Sample Northwind database https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/sql/linq/downloading-sample-databases
// MS SQL data access library https://www.nuget.org/packages/Microsoft.Data.SqlClient/
// System.Data.SqlClient vs Microsoft.Data.SqlClient https://devblogs.microsoft.com/dotnet/introducing-the-new-microsoftdatasqlclient/
// Transactions https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/local-transactions

using System.Data;
using Microsoft.Data.SqlClient;

const string ConnectionString = "Server=tcp:learn-m9-db-server.database.windows.net,1433;Initial Catalog=learn-m9-db;Persist Security Info=False;User ID=sk;Password=December2022#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

//ExecuteSimpleQuery();
//ExecuteParametrizedQuery();
//ExecuteNonQuery();
//CallStoredProcedure();
//Transactions();

// Methods
static void ExecuteSimpleQuery()
{
    using var connection = new SqlConnection(ConnectionString);

    var query = "select EmployeeID, LastName, FirstName, Title, BirthDate, City from Employees";

    var command = new SqlCommand(query, connection);

    connection.Open();

    using var dataReader = command.ExecuteReader();

    while (dataReader.Read())
    {
        Console.Write($"Employee ID: {dataReader[0]} | ");
        Console.Write($"Last Name: {dataReader[1]} | ");
        Console.Write($"First Name: {dataReader[2]} | ");
        Console.Write($"Title: {dataReader[3]} | ");
        Console.Write($"Birth Date: {dataReader[4]} | ");
        Console.Write($"City: {dataReader[5]}");
        Console.WriteLine();
    }
}

static void ExecuteParametrizedQuery()
{
    // We want to find all employees from London
    var cityToQuery = "London";

    using var connection = new SqlConnection(ConnectionString);

    //var query = $"select EmployeeID, LastName, FirstName, Title, BirthDate, City " +
    //    $"from Employees " +
    //    $"where City = '{cityToQuery}'"; -- BAD APPROACH!

    var query = "select EmployeeID, LastName, FirstName, Title, BirthDate, City " +
        "from Employees " +
        "where City = @city";

    var cityParameter = new SqlParameter
    {
        DbType = DbType.String,
        Direction = ParameterDirection.Input,
        ParameterName = "@city",
        Value = cityToQuery
    };

    var command = new SqlCommand(query, connection);
    command.Parameters.Add(cityParameter);

    connection.Open();

    using var dataReader = command.ExecuteReader();

    while (dataReader.Read())
    {
        Console.Write($"Employee ID: {dataReader[0]} | ");
        Console.Write($"Last Name: {dataReader[1]} | ");
        Console.Write($"First Name: {dataReader[2]} | ");
        Console.Write($"Title: {dataReader[3]} | ");
        Console.Write($"Birth Date: {dataReader[4]} | ");
        Console.Write($"City: {dataReader[5]}");
        Console.WriteLine();
    }
}

static void ExecuteNonQuery()
{
    using var connection = new SqlConnection(ConnectionString);

    var query = "insert into Employees(LastName, FirstName, Title) " +
        "values (@lastName, @firstName, @title)";

    var lastNameParameter = new SqlParameter
    {
        DbType = DbType.String,
        ParameterName = "@lastName",
        Value = "Simpson"
    };

    var firstNameParameter = new SqlParameter
    {
        DbType = DbType.String,
        ParameterName = "@firstName",
        Value = "Homer"
    };

    var titleParameter = new SqlParameter
    {
        DbType = DbType.String,
        ParameterName = "@title",
        Value = "Sr. Director"
    };

    var command = new SqlCommand(query, connection);
    command.Parameters.Add(lastNameParameter);
    command.Parameters.Add(firstNameParameter);
    command.Parameters.Add(titleParameter);

    connection.Open();

    command.ExecuteNonQuery();
}

static void CallStoredProcedure()
{
    string storedProcedureName = "CustOrderHist";
    string customerID = "QUICK";

    using var connection = new SqlConnection(ConnectionString);

    var customerIDParameter = new SqlParameter
    {
        Direction = ParameterDirection.Input,
        ParameterName = "@CustomerID",
        Value = customerID
    };

    var command = new SqlCommand(storedProcedureName, connection);
    command.CommandType = CommandType.StoredProcedure;
    command.Parameters.Add(customerIDParameter);

    connection.Open();

    using var dataReader = command.ExecuteReader();

    while (dataReader.Read())
    {
        Console.Write($"Product Name: {dataReader[0]} | ");
        Console.Write($"Total: {dataReader[1]}");
        Console.WriteLine();
    }

}

static void Transactions()
{
    using var connection = new SqlConnection(ConnectionString);

    connection.Open();

    SqlTransaction sqlTran = connection.BeginTransaction();

    var query = "insert into Employees(LastName, FirstName, Title) " +
        "values (@lastName, @firstName, @title)";

    var command1 = new SqlCommand(query, connection);
    command1.Parameters.Add(new SqlParameter("@lastName", "Potter"));
    command1.Parameters.Add(new SqlParameter("@firstName", "Harry"));
    command1.Parameters.Add(new SqlParameter("@title", "Jr. Magician"));

    command1.Transaction = sqlTran;

    var command2 = new SqlCommand(query, connection);
    command2.Parameters.Add(new SqlParameter("@lastName", "SquarePants"));
    command2.Parameters.Add(new SqlParameter("@firstName", "SpondgeBob"));
    command2.Parameters.Add(new SqlParameter("@title", "Yellow Spondge"));

    command2.Transaction = sqlTran;

    try
    {
        command1.ExecuteNonQuery();
        command2.ExecuteNonQuery();

        sqlTran.Commit();
    }
    catch (Exception ex)
    {
        // Handle the exception if the transaction fails to commit.
        Console.WriteLine(ex.Message);

        try
        {
            // Attempt to roll back the transaction.
            sqlTran.Rollback();
        }
        catch (Exception exRollback)
        {
            // Throws an InvalidOperationException if the connection
            // is closed or the transaction has already been rolled
            // back on the server.
            Console.WriteLine(exRollback.Message);
        }
    }

}