using System.Xml.Serialization;

namespace SoftJail.DataProcessor.ExportDto
{
    [XmlType("Message")]
    public class MailDto
    {
        [XmlElement("Description")]
        public string Description { get; set; }
    }
}