// XmlDocument https://learn.microsoft.com/en-us/dotnet/api/system.xml.xmldocument?view=net-7.0

using System.Globalization;
using System.Text;
using System.Xml;
using Shared;
using Shared.XmlModels;

//ReadDocumentContent();
//CreateDocumentContent();
//UpdateDocumentContent();

static void ReadDocumentContent()
{
    var resultBuilder = new StringBuilder();

    //XmlElement
    var rootElement = GetOrdersDocument().DocumentElement;

    //Recursively iterate through the nodes and show stats
    ReadNodes(rootElement, resultBuilder);

    Console.WriteLine(resultBuilder.ToString());
}

static void CreateDocumentContent()
{
    var doc = new XmlDocument();

    //XmlElement
    var root = doc.CreateElement("Orders", Constants.OrdersNamespace);
    var order1 = doc.CreateElement("Order", Constants.OrdersNamespace);
    var item1 = doc.CreateElement("Item", Constants.OrdersNamespace);
    var customerName1 = doc.CreateElement("CustomerName", Constants.OrdersNamespace);
    var count1 = doc.CreateElement("Count", Constants.OrdersNamespace);
    var totalPrice1 = doc.CreateElement("ns1:TotalPrice", Constants.PricesNamespace);

    //XmlAttribute
    var discount1 = doc.CreateAttribute("discount");

    item1.InnerText = "Duff Beer";
    customerName1.InnerText = "Homer Simpson";
    count1.InnerText = "4";
    totalPrice1.InnerText = "12.50";
    discount1.InnerText = "true";

    doc.AppendChild(root);
    root.AppendChild(order1);
    order1.AppendChild(item1);
    order1.AppendChild(customerName1);
    order1.AppendChild(count1);

    totalPrice1.Attributes.Append(discount1);
    order1.AppendChild(totalPrice1);

    Console.WriteLine(doc.OuterXml);
}

static void UpdateDocumentContent()
{
    XmlDocument doc = GetOrdersDocument();

    //Increase all totals to 10%;
    var totals = doc.GetElementsByTagName("TotalPrice", Constants.PricesNamespace);

    foreach (XmlElement total in totals)
    {
        try
        {
            var totalNum = decimal.Parse(total.InnerText);
            total.InnerText = (1.1M * totalNum).ToString(CultureInfo.InvariantCulture);
        }
        catch (Exception)
        {
            //Do nothing
        }
    }

    Console.WriteLine(doc.OuterXml);
}

static XmlDocument GetOrdersDocument()
{
    var doc = new XmlDocument();

    doc.Load(Path.Combine("Files", "Orders.xml"));

    return doc;
}

static void ReadNodes(XmlNode parentNode, StringBuilder resultBuilder)
{
    foreach (XmlNode node in parentNode)
    {
        resultBuilder.AppendLine(FormatNodeStats(node));

        if (node.HasChildNodes)
        {
            ReadNodes(node, resultBuilder);
        }
    }
}

static string FormatNodeStats(XmlNode node)
{
    return $"Node Name: {node.Name}\r\n" +
            $"Node Namespace: {node.NamespaceURI}\r\n" +
            $"Node Text: {node.InnerText}\r\n" +
            $"Node Raw Xml: {node.InnerXml}\r\n";
}
