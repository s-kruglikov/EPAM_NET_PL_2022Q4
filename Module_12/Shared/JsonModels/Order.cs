using System.Text.Json.Serialization;
using Shared.XmlModels;

namespace Shared.JsonModels
{
    /// <summary>
    /// Order DTO
    /// </summary>
    [Serializable]
    public class Order
    {
        //[JsonIgnore]
        public int Id { get; set; }

        public string Item { get; set; }

        public string CustomerName { get; set; }

        //[JsonPropertyName("ItemsCount")]
        public int Count { get; set; }

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

