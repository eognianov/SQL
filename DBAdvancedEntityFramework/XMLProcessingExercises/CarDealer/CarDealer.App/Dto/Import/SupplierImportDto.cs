namespace CarDealer.App.Dto.Import
{
    using System.Xml.Serialization;

    [XmlType("supplier")]
    public class SupplierImportDto
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("is-importer")]
        public bool IsImporter { get; set; }
    }
}