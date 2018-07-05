namespace P02_DatabaseFirst.Data.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class EmployeeProjectConfiguration : IEntityTypeConfiguration<EmployeeProject>
    {
        public void Configure(EntityTypeBuilder<EmployeeProject> builder)
        {
            builder.HasKey(e => new { e.EmployeeId, e.ProjectId });

            builder.Property(e => e.EmployeeId)
                .HasColumnName("EmployeeID");

            builder.Property(e => e.ProjectId)
                .HasColumnName("ProjectID");

            builder.HasOne(d => d.Employee)
                .WithMany(p => p.EmployeesProjects)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeesProjects_Employees");

            builder.HasOne(d => d.Project)
                .WithMany(p => p.EmployeesProjects)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_EmployeesProjects_Projects");
        }
    }
}