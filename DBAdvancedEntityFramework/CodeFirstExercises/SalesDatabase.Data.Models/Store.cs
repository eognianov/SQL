using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace P03_SalesDatabase.Data.Models
{
    public class Store
    {
        public Store()
        {
            this.Sales = new List<Sale>();
        }
        [Key]
        public int StoreId { get; set; }

        [Column(TypeName = "nvarchar(80)")]
        public string Name { get; set; }

        public ICollection<Sale> Sales { get; set; }
    }
}
