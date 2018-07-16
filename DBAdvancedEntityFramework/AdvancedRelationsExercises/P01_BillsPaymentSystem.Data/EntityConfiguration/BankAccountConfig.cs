using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_BillsPaymentSystem.Data.Models;

namespace P01_BillsPaymentSystem.Data.EntityConfiguration
{
    public class BankAccountConfig:IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasOne(x => x.PaymentMethod).WithOne(x => x.BankAccount)
                .HasForeignKey<PaymentMethod>(x => x.BankAccountId);

            builder.HasKey(x => x.BankAccountId);
            

            builder.Property(e => e.BankName)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode();

            builder.Property(e => e.SWIFTCode)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Ignore(e => e.PaymentMethodId);
        }
    }
}
