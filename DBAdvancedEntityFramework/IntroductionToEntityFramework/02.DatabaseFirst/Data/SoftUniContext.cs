namespace P02_DatabaseFirst.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Configurations;

    public class SoftUniContext : DbContext
    {
        public SoftUniContext()
        {
        }

        public SoftUniContext(DbContextOptions<SoftUniContext> options)
            : base(options)
        {
        }

        public  DbSet<Address> Addresses { get; set; }

        public  DbSet<Department> Departments { get; set; }

        public  DbSet<Employee> Employees { get; set; }

        public  DbSet<EmployeeProject> EmployeesProjects { get; set; }

        public  DbSet<Project> Projects { get; set; }

        public  DbSet<Town> Towns { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuraiton.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AddressConfiguration());

            builder.ApplyConfiguration(new DepartmentConfiguration());

            builder.ApplyConfiguration(new EmployeeConfiguration());

            builder.ApplyConfiguration(new EmployeeProjectConfiguration());

            builder.ApplyConfiguration(new ProjectConfiguration());

            builder.ApplyConfiguration(new TownConfiguration());
        }
    }
}