using System.Xml.Serialization;

namespace Shared.XmlModels
{
    /// <summary>
    /// Order DTO
    /// </summary>
    [Serializable]
    public class Order
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("Item")]
        public string Item { get; set; }

        [XmlElement("CustomerName")]
        public string CustomerName { get; set; }

        [XmlElement("Count")]
        public int Count { get; set; }

        [XmlElement("TotalPrice", Namespace = Constants.PricesNamespace)]
        public Price Price { get; set; }

        public override string ToString()
        {
            return "*** Order *** \r\n"
                + $"Id: {Id}\r\n"
                + $"Item: {Item}\r\n"
                + $"CustomerName: {CustomerName}\r\n"
                + $"Count: {Count}\r\n"
                + $"TotalPrice: {Price.TotalPrice}\r\n"
                + $"Discount: {Price.Discount}\r\n";
        }
    }
}