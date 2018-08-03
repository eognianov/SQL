namespace CarDealer.Data
{
    using EntityConfiguration;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CarDealerContext : DbContext
    {
        public CarDealerContext()
        {
        }

        public CarDealerContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<PartCar> PartCars { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CarConfiguration());

            builder.ApplyConfiguration(new CustomerConfiguration());

            builder.ApplyConfiguration(new PartCarConfiguration());

            builder.ApplyConfiguration(new PartConfiguration());

            builder.ApplyConfiguration(new SaleConfiguration());

            builder.ApplyConfiguration(new SupplierConfiguration());
        }
    }
}