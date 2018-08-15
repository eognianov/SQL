using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;


namespace FastFood.DataProcessor.Dto.Import
{
    [XmlType("Item")]
    public class OrderItemDto
    {
        [Required]
        [XmlElement("Name")]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        [XmlElement("Quantity")]
        public int Quantity { get; set; }
    }
}
