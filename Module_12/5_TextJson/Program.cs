// JSON serialization and deserialization https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/overview?source=recommendations
// Traversing JSON nodes https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json/use-dom-utf8jsonreader-utf8jsonwriter?pivots=dotnet-7-0

using System.Text.Json;
using System.Text.Json.Nodes;
using Shared.JsonModels;

//SerializeToJson();
//DeserializeJson();
//TraverseJsonNodes();

static void SerializeToJson()
{
    var objectToSerialize = new { orders = OrdersCollection.GetOrders() };

    var serializedValue = JsonSerializer.Serialize(objectToSerialize, new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
    });

    Console.WriteLine(serializedValue);
}

static void DeserializeJson()
{
    var jsonString = File.ReadAllText(Path.Combine("Files", "Orders.json"));

    var deserializedOrders = JsonSerializer.Deserialize<OrdersCollection>(jsonString, new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
    });

    foreach (var item in deserializedOrders.Orders)
    {
        Console.WriteLine(item);
    }
}

static void TraverseJsonNodes()
{
    // Show all customers from orders
    var jsonString = File.ReadAllText(Path.Combine("Files", "Orders.json"));

    JsonNode ordersNodes = JsonNode.Parse(jsonString)!;

    foreach (var item in ordersNodes["orders"] as JsonArray)
    {
        Console.WriteLine(item["customerName"]);
    }
}