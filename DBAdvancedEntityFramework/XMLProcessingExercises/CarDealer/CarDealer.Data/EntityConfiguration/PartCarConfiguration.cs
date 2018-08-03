namespace CarDealer.Data.EntityConfiguration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Models;

    internal class PartCarConfiguration : IEntityTypeConfiguration<PartCar>
    {
        public void Configure(EntityTypeBuilder<PartCar> builder)
        {
            builder.HasKey(pc => new { pc.CarId, pc.PartId });

            builder.HasOne(pc => pc.Car)
                .WithMany(c => c.PartCars)
                .HasForeignKey(pc => pc.CarId);
            
            builder.HasOne(pc => pc.Part)
                .WithMany(p => p.PartCars)
                .HasForeignKey(pc => pc.PartId);
        }
    }
}