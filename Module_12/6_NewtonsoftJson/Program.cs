// Newtonsoft https://www.newtonsoft.com/json

using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared;
using Shared.JsonModels;

//SerializeToJson();
//DeserializeJson();
//TraverseJsonNodes();

static void SerializeToJson()
{
    var objectToSerialize = new OrdersCollection { Orders = OrdersCollection.GetOrders() };

    var json = JsonConvert.SerializeObject(objectToSerialize);

    Console.WriteLine(json);

    // Another more flexible option
    var serializer = new Newtonsoft.Json.JsonSerializer();
    serializer.Formatting = Formatting.Indented;

    using var memoryStream = new MemoryStream();
    using var streamWriter = new StreamWriter(memoryStream);
    using var jsonWriter = new JsonTextWriter(streamWriter);

    serializer.Serialize(jsonWriter, objectToSerialize);

    jsonWriter.Flush();
    streamWriter.Flush();

    Helpers.OutputStreamToConsole(memoryStream);
}

static void DeserializeJson()
{
    var jsonString = File.ReadAllText(Path.Combine("Files", "Orders.json"));

    var orders = JsonConvert.DeserializeObject<OrdersCollection>(jsonString);

    foreach (var item in orders.Orders)
    {
        Console.WriteLine(item);
    }
}

static void TraverseJsonNodes()
{
    // Show all customers from orders

    var jsonString = File.ReadAllText(Path.Combine("Files", "Orders.json"));

    JObject ordersRoot = JObject.Parse(jsonString);

    foreach (var item in ordersRoot["orders"] as JArray)
    {
        Console.WriteLine(item["customerName"]);
    }
}