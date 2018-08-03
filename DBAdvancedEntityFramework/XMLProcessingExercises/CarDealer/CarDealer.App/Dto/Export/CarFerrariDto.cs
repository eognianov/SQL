namespace CarDealer.App.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("car")]
    public class CarFerrariDto
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public int TravelledDistance { get; set; }
    }
}