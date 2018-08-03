using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.App.Dto.Export
{
    using System.Xml.Serialization;

    [XmlRoot("users")]
    public class UserRootDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("user")]
        public UserExportDto[] Users { get; set; }
    }
}
