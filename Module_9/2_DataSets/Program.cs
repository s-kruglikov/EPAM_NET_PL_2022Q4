// https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/populating-a-dataset-from-a-dataadapter

using System.Data;
using Microsoft.Data.SqlClient;

const string ConnectionString = "Server=tcp:learn-m9-db-server.database.windows.net,1433;Initial Catalog=learn-m9-db;Persist Security Info=False;User ID=sk;Password=December2022#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

string selectQuery = "select * from Employees";

using var connection = new SqlConnection(ConnectionString);

connection.Open();

var adapter = new SqlDataAdapter(selectQuery, connection);

var employeesDataSet = new DataSet();

adapter.Fill(employeesDataSet);

// See the content of our dataset
foreach (DataTable table in employeesDataSet.Tables)
{
    Console.WriteLine($"Table name: {table.TableName}");
    Console.WriteLine();

    foreach (DataRow row in table.Rows)
    {
        foreach (DataColumn column in table.Columns)
        {
            Console.Write($"{column.ColumnName}: ");
            Console.WriteLine($"{row[column].ToString()}");
        }

        Console.WriteLine();
    }
}

// Add a row to the datatable
var employeesDataTable = employeesDataSet.Tables[0];

var newDataRow = employeesDataTable.NewRow();
newDataRow["LastName"] = "Simpson";
newDataRow["FirstName"] = "Bart";
newDataRow["Title"] = "Jr. .NET Engineer";

employeesDataTable.Rows.Add(newDataRow);

var commandBuilder = new SqlCommandBuilder(adapter);
adapter.Update(employeesDataSet); // here we can update single table or the dataset

// See commands
Console.WriteLine($"Update command: {commandBuilder.GetUpdateCommand().CommandText}");
Console.WriteLine($"Insert command: {commandBuilder.GetInsertCommand().CommandText}");
Console.WriteLine($"Delete command: {commandBuilder.GetDeleteCommand().CommandText}");