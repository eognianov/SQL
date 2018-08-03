namespace CarDealer.App.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("part")]
    public class PartExportDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("price")]
        public decimal Price { get; set; }
    }
}