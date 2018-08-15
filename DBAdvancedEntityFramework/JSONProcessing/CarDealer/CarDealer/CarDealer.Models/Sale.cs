using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CarDealer.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public decimal Discount { get; set; }

        [ForeignKey("Car")]
        public int Car_Id { get; set; }

        public virtual Car Car { get; set; }

        [ForeignKey("Customer")]
        public int Customer_Id { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
