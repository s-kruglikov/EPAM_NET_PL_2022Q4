// XmlSerializer https://learn.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?view=net-7.0

using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Shared;
using Shared.XmlModels;

//SerializeToXml();
//SerializeToXmlWithSettings();
//DeserializeXml();

static void SerializeToXml()
{
    var orders = OrdersCollection.GetOrders();

    using (var resultStream = new MemoryStream())
    {
        var serializer = new XmlSerializer(typeof(OrdersCollection));

        serializer.Serialize(resultStream, orders);

        Helpers.OutputStreamToConsole(resultStream);
    }
}

static void SerializeToXmlWithSettings()
{
    var orders = OrdersCollection.GetOrders();

    using (var resultStream = new MemoryStream())
    {
        var serializerNamespaces = new XmlSerializerNamespaces();
        serializerNamespaces.Add(string.Empty, Constants.OrdersNamespace);
        serializerNamespaces.Add("ns1", Constants.PricesNamespace);

        var serializer = new XmlSerializer(typeof(OrdersCollection));

        var writerSettings = new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = true };
        var resultWriter = XmlWriter.Create(resultStream, writerSettings);

        serializer.Serialize(resultWriter, orders, serializerNamespaces);

        Helpers.OutputStreamToConsole(resultStream);
    }
}

static void DeserializeXml()
{
    var ordersStream = File.Open(Path.Combine("Files", "Orders.xml"), FileMode.Open);

    var serializer = new XmlSerializer(typeof(OrdersCollection));

    var orders = serializer.Deserialize(ordersStream) as OrdersCollection;

    if(orders is not null)
    {
        var resultBuilder = new StringBuilder();
        foreach (var order in orders)
        {
            resultBuilder.Append(order);
        }

        Console.WriteLine(resultBuilder.ToString());
    }
}