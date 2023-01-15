using System.Xml.Serialization;

namespace Shared.XmlModels
{
    [Serializable]
    public class Price
    {
        [XmlText]
        public decimal TotalPrice { get; set; }

        [XmlAttribute(attributeName: "discount", DataType = "boolean")]
        public bool Discount { get; set; }
    }
}
