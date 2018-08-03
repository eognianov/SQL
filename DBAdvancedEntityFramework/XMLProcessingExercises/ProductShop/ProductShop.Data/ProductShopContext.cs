using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ProductShop.Models;

namespace ProductShop.Data
{
    public class ProductShopContext : DbContext
    {
        public ProductShopContext(DbContextOptions options) : base(options)
        {
        }

        public ProductShopContext()
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConfigurationString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryProduct>().HasKey(x => new {x.CategoryId, x.ProductId});

            modelBuilder.Entity<User>(entity =>
                {
                    entity.HasMany(x => x.ProductSold).WithOne(x => x.Seller).HasForeignKey(x => x.SellerId);
                    entity.HasMany(x => x.ProductBought).WithOne(x => x.Buyer).HasForeignKey(x => x.BuyerId);
                });
        }
    }
}
