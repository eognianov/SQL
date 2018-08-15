using System;
using System.Collections.Generic;
using System.Text;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarDealer.Data
{
    public class SaleConfig:IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasOne(s => s.Car).WithMany(c => c.Sales).HasForeignKey(s => s.Car_Id);

            builder.HasOne(s => s.Customer).WithMany(c => c.Sales).HasForeignKey(s => s.Customer_Id);
        }
    }
}
