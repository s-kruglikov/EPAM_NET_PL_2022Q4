// Scaffold models from existing DB https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/?tabs=dotnet-core-cli

using System.Data;
using _3_EntityFramework;
using _3_EntityFramework.Entities;
using _3_EntityFramework.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

//ExecuteQueryData();
//ExecuteModifyData();
//ExecuteConditionalQueryData();
//ExecuteRawSql();
//ExecuteStoredProcedure();

static void ExecuteQueryData()
{
    using var context = new ApplicationContext();

    foreach (var region in context.Region.Include(i => i.Territories)) // 
    {
        Console.WriteLine($"Region Id: {region.RegionID}");
        Console.WriteLine($"Region Description: {region.RegionDescription}");
        Console.WriteLine($"Region Territories");
        foreach (var territory in region.Territories)
        {
            Console.WriteLine($"-{territory.TerritoryID}-{territory.TerritoryDescription}");
        }
    }
}

static void ExecuteModifyData()
{
    using var context = new ApplicationContext();

    var newTerritory = new Territory
    {
        TerritoryID = "00000",
        TerritoryDescription = "Test territory"
    };

    var newRegion = new Region
    {
        RegionID = 10,
        RegionDescription = "Test region",
        Territories = new List<Territory> { newTerritory }
    };

    context.Region.Add(newRegion);

    context.SaveChanges();
}

static void ExecuteConditionalQueryData()
{
    using var context = new ApplicationContext();

    //var region = context.Region.FirstOrDefault(f => f.RegionDescription == "Test region");
    var region = context.Region.Include(i => i.Territories)
        .FirstOrDefault(f => f.RegionDescription == "Test region");

    if (region is not null)
    {
        Console.WriteLine($"Region Id: {region.RegionID}");
        Console.WriteLine($"Region Description: {region.RegionDescription}");
        Console.WriteLine($"Region Territories");
        foreach (var territory in region.Territories)
        {
            Console.WriteLine($"-{territory.TerritoryID}-{territory.TerritoryDescription}");
        }
    }
}

static void ExecuteRawSql()
{
    using var context = new ApplicationContext();

    var query = "select * from Territories where RegionID = @regionID";

    var parameter = new SqlParameter
    {
        ParameterName = "@regionID",
        Value = 4
    };

    var territories = context.Territories.FromSqlRaw<Territory>(query, parameter);

    foreach (var territory in territories)
    {
        Console.WriteLine($"{territory.TerritoryID} - {territory.TerritoryDescription}");
    }
}

static void ExecuteStoredProcedure()
{
    string storedProcedureName = "CustOrderHist";
    string customerID = "QUICK";

    using var context = new ApplicationContext();

    var customerIDParameter = new SqlParameter
    {
        Direction = ParameterDirection.Input,
        ParameterName = "@CustomerID",
        Value = customerID
    };

    var result = context.CustOrderHistProcedureResponses
        .FromSqlRaw<CustOrderHistProcedureResponse>($"{storedProcedureName} @CustomerID", customerIDParameter);

    foreach (var item in result)
    {
        Console.WriteLine($"{item.ProductName} - {item.Total}");
    }
}

