using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.App.Dto.Export
{
    using System.Xml.Serialization;

    [XmlType("user")]
    public class UserExportDto
    {
        [XmlAttribute("first-name")]
        public string FirstName { get; set; }

        [XmlAttribute("last-name")]
        public string LastName { get; set; }

        [XmlAttribute("age")]
        public string Age { get; set; }

        [XmlElement("sold-products")]
        public ProductSoldRootDto Products { get; set; }
    }
}
