using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Data.ModelsConfiguration
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Id).IsUnique();

            builder.HasOne(c => c.Owner)
                .WithMany()
                .HasForeignKey(c => c.OwnerId);

            builder.HasMany(c => c.Accidents)
                .WithOne(a => a.Car)
                .HasForeignKey(a => a.CarId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
