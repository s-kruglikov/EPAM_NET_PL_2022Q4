// XmlReader https://learn.microsoft.com/en-us/dotnet/api/system.xml.xmlreader?view=net-7.0
// XmlWriter https://learn.microsoft.com/en-us/dotnet/api/system.xml.xmlwriter?view=net-7.0

using System.Text;
using System.Xml;
using System.Xml.Schema;
using Shared;
using Shared.XmlModels;

//WriteXmlWithWriter();
//ReadWithXmlReader();
//ChainReaderWriter();

static void WriteXmlWithWriter()
{
    var writerSettings = new XmlWriterSettings
    {
        ConformanceLevel = ConformanceLevel.Document,
        Indent = true,
        IndentChars = "\t",
        NewLineChars = "\r\n",
        OmitXmlDeclaration = true
    };

    //Use MemoryStream only for demo purpose
    using (var memoryStream = new MemoryStream())
    {
        var writer = XmlWriter.Create(memoryStream, writerSettings);

        FillData(writer);

        Helpers.OutputStreamToConsole(memoryStream);
    }
}

static void ReadWithXmlReader()
{
    var result = new StringBuilder();

    using var ordersStream = File.Open(Path.Combine("Files", "Orders.xml"), FileMode.Open);

    var settings = new XmlReaderSettings();
    using var reader = XmlReader.Create(ordersStream, settings);

    while (reader.Read())
    {
        result.Append(FormatElementStats(reader));

        if (reader.HasAttributes)
            while (reader.MoveToNextAttribute())
            {
                result.Append(FormatElementStats(reader));
            }
    }

    Console.WriteLine(result.ToString());
}

static void ChainReaderWriter()
{
    var ordersStream = File.Open(Path.Combine("Files", "Orders.xml"), FileMode.Open);
    var resultStream = new MemoryStream();

    var writerSettings = new XmlWriterSettings
    {
        ConformanceLevel = ConformanceLevel.Fragment
    };

    var readerSettings = new XmlReaderSettings
    {
        ConformanceLevel = ConformanceLevel.Fragment,
        ValidationType = ValidationType.None,
        ValidationFlags = XmlSchemaValidationFlags.None,
    };

    using var reader = XmlReader.Create(ordersStream, readerSettings);
    using var writer = XmlWriter.Create(resultStream, writerSettings);

    // Simply copies content but logic can be extended.
    while (reader.Read())
    {
        if (reader.NodeType == XmlNodeType.Element)
        {
            writer.WriteStartElement(reader.Prefix, reader.LocalName, reader.NamespaceURI);
        }
        if (reader.NodeType == XmlNodeType.Text)
        {
            writer.WriteString(reader.Value);
        }
        if (reader.NodeType == XmlNodeType.EndElement)
        {
            writer.WriteEndElement();
        }
    }

    writer.Flush();

    Helpers.OutputStreamToConsole(resultStream);
}

static void FillData(XmlWriter writer)
{
    writer.WriteStartDocument();
      writer.WriteStartElement("Orders", Constants.OrdersNamespace);
        writer.WriteAttributeString("xmlns", "ns1", null, Constants.PricesNamespace);
        writer.WriteComment("Collection of orders");
          writer.WriteStartElement("Order", null);
            writer.WriteAttributeString("id", "1");
            writer.WriteElementString("Item", "Duff Beer");
            writer.WriteElementString("CustomerName", "Homer Simpson");
            writer.WriteElementString("Count", "4");
            writer.WriteStartElement("ns1", "TotalPrice", null);
              writer.WriteAttributeString("discount", "true");
              writer.WriteString("12.50");
            writer.WriteEndElement();
          writer.WriteEndElement();

          writer.WriteStartElement("Order", null);
            writer.WriteAttributeString("id", "2");
            writer.WriteElementString("Item", "Cherry Pie");
            writer.WriteElementString("CustomerName", "Marge Simpson");
            writer.WriteElementString("Count", "1");
              writer.WriteStartElement("ns1", "TotalPrice", null);
              writer.WriteAttributeString("discount", "false");
              writer.WriteString("5.15");
            writer.WriteEndElement();
          writer.WriteEndElement();
        writer.WriteEndElement();
    writer.WriteEndDocument();
    writer.Flush();
}

static string FormatElementStats(XmlReader reader)
{
    var result = new StringBuilder();

    result.Append($"Type: '{reader.NodeType}'\t\t");
    if (!string.IsNullOrEmpty(reader.LocalName))
        result.Append($"Name: '{reader.LocalName}'\t\t");

    if (!string.IsNullOrEmpty(reader.Value))
        result.Append($"Value: '{reader.Value}'\t\t");

    result.Append(Environment.NewLine);

    return result.ToString();
}