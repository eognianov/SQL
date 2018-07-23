using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Office.Models;

namespace Office.Data
{
    public class OfficeContext :DbContext
    {
        public OfficeContext(DbContextOptions options) : base(options)
        {
        }

        public OfficeContext()
        {}

        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
                {
                    entity.HasOne(x => x.Manager).WithMany(x => x.ManagerEmployees).HasForeignKey(x => x.ManagerId);
                });
        }

    }
}
