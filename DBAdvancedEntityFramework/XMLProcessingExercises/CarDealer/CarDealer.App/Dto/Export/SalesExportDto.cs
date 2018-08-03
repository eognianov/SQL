namespace CarDealer.App.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("sale")]
    public class SalesExportDto
    {
        [XmlAttribute("make")]
        public string Make { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public int TravelledDistance { get; set; }

        [XmlElement("customer-name")]
        public string CustomerName { get; set; }

        [XmlElement("discount")]
        public int Discount { get; set; }

        [XmlElement("price")]
        public decimal Price { get; set; }

        [XmlElement("price-with-discount")]
        public decimal PriceDiscount
        {
            get
            {
                return this.Price - (this.Price* this.Discount / 100m);
            }
            set { }
        }
    }
}