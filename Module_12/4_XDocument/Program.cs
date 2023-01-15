using System.Text;
using System.Xml.Linq;
using Shared;
using Shared.XmlModels;

//ReadDocumentContent();
//CreateDocumentContent();
//AddContentToDocument();
//UpdateDocumentContent();

static void ReadDocumentContent()
{
    XNamespace defaultNamespace = Constants.OrdersNamespace;

    var result = new StringBuilder();
    result.AppendLine("Today we have orders:");

    var doc = XDocument.Load(Path.Combine("Files", "Orders.xml"));

    foreach (XElement order in doc.Element(defaultNamespace + "Orders").Elements(defaultNamespace + "Order"))
    {
        result.AppendLine(string.Format("{0} for {1}",
                                        order.Element(defaultNamespace + "Item").Value,
                                        order.Element(defaultNamespace + "CustomerName").Value));
    }

    Console.WriteLine(result.ToString());
}

static void CreateDocumentContent()
{
    XNamespace defaultNamespace = Constants.OrdersNamespace;
    XNamespace pricesNamespace = Constants.PricesNamespace;

    XDocument doc = new XDocument();

    XElement ordersElement = new XElement(defaultNamespace + "Orders");
    ordersElement.Add(new XComment("Collection of orders"));

    XElement orderElement = new XElement(defaultNamespace + "Order");
    orderElement.Add(new XAttribute("id", 1));
    orderElement.Add(new XElement(defaultNamespace + "Item", "Duff Beer"));
    orderElement.Add(new XElement(defaultNamespace + "CustomerName", "Homer Simpson"));
    orderElement.Add(new XElement(defaultNamespace + "Count", "4"));

    XElement totalPriceElement = new XElement(pricesNamespace + "TotalPrice", 12.50);
    totalPriceElement.Add(new XAttribute("discount", true));

    orderElement.Add(totalPriceElement);
    ordersElement.Add(orderElement);
    doc.Add(ordersElement);

    Console.WriteLine(doc.ToString());
}

static void AddContentToDocument()
{
    XNamespace defaultNamespace = Constants.OrdersNamespace;
    XNamespace pricesNamespace = Constants.PricesNamespace;

    var doc = XDocument.Load(Path.Combine("Files", "Orders.xml"));
    var order = new Order
    {
        Id = 3,
        Item = "Chocolate bar",
        CustomerName = "Bart Simpson",
        Count = 1,
        Price = new Price
        {
            TotalPrice = 1.10M,
            Discount = false
        }
    };

    //Add new order to document
    doc.Descendants(defaultNamespace + "Orders").First().Add(CreateOrderElement(order));

    Console.WriteLine(doc.ToString());

    XElement CreateOrderElement(Order orderObj)
    {
        _ = orderObj ?? throw new ArgumentNullException(nameof(orderObj));

        var orderElement = new XElement(defaultNamespace + "Order");
        orderElement.Add(new XAttribute("id", orderObj.Id));
        orderElement.Add(new XElement(defaultNamespace + "Item", orderObj.Item));
        orderElement.Add(new XElement(defaultNamespace + "CustomerName", orderObj.CustomerName));
        orderElement.Add(new XElement(defaultNamespace + "Count", orderObj.Count));

        var totalPriceElement = new XElement(pricesNamespace + "TotalPrice", orderObj.Price.TotalPrice);
        totalPriceElement.Add(new XAttribute("discount", orderObj.Price.Discount));

        orderElement.Add(totalPriceElement);

        return orderElement;
    }
}

static void UpdateDocumentContent()
{
    //Move TotalPrice and Discount under Order node.
    XNamespace defaultNamespace = Constants.OrdersNamespace;
    XNamespace pricesNamespace = Constants.PricesNamespace;

    var doc = XDocument.Load(Path.Combine("Files", "Orders.xml"));

    foreach (var order in doc.Element(defaultNamespace + "Orders")
                                .Elements(defaultNamespace + "Order")
                                .ToList())
    {
        var discount = order.Element(pricesNamespace + "TotalPrice").Attribute("discount").Value;
        var totalPrice = order.Element(pricesNamespace + "TotalPrice").Value;

        order.Element(pricesNamespace + "TotalPrice").Remove();

        order.Add(new XElement(defaultNamespace + "Discount", discount));
        order.Add(new XElement(defaultNamespace + "TotalPrice", totalPrice));
    }

    Console.WriteLine(doc.ToString());
}