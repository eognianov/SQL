using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.App.Dto.Export
{
    [XmlType("sold-products")]
    public class ProductSoldRootDto
    {
        [XmlAttribute("count")]
        public int Count { get; set; }

        [XmlElement("product")]
        public SoldProductDTO[] ProductSoldDtos { get; set; }
    }
}
