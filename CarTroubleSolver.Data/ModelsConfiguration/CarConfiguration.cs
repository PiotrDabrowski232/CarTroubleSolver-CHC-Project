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

            builder.HasOne(c => c.Owner)
                .WithMany(c => c.Cars)
                .HasForeignKey(c => c.Id);
        }
    }
}
