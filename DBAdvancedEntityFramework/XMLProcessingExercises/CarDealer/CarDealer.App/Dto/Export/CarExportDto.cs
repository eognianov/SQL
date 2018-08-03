namespace CarDealer.App.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("car")]
    public class CarExportDto
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("travelled-distance")]
        public int TravelledDistance { get; set; }
    }
}