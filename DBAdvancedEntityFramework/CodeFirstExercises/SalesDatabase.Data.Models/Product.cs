using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P03_SalesDatabase.Data.Models
{
    public class Product
    {
        public Product()
        {
            this.Sales = new HashSet<Sale>();
        }
        [Key]
        public int ProductId { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        public string Description { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
