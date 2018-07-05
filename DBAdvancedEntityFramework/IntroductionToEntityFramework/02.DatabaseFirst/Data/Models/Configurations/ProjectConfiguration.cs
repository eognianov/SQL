namespace P02_DatabaseFirst.Data.Models.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(e => e.ProjectId);

            builder.Property(e => e.ProjectId)
                .HasColumnName("ProjectID");

            builder.Property(e => e.Description)
                .HasColumnType("ntext");

            builder.Property(e => e.EndDate)
                .HasColumnType("smalldatetime");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.StartDate)
                .HasColumnType("smalldatetime");
        }
    }
}