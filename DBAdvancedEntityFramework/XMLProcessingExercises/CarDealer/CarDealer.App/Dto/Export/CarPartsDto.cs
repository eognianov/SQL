namespace CarDealer.App.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("car")]
    public class CarPartsDto
    {
        [XmlAttribute("made")]
        public string Made { get; set; }

        [XmlAttribute("model")]
        public string Model { get; set; }

        [XmlAttribute("travelled-distance")]
        public int TravelledDistance { get; set; }

        [XmlArray("parts")]
        public PartExportDto[] PartExportDtos { get; set; }
    }
}