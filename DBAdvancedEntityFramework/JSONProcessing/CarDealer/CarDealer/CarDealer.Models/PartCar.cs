using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarDealer.Models
{
    public class PartCar
    {
        [ForeignKey("Part")]
        public int Part_Id { get; set; }
        public virtual Part Part { get; set; }

        [ForeignKey("Car")]
        public int Car_Id { get; set; }
        public virtual Car Car { get; set; }


    }
}
