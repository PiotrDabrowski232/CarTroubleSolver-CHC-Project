using CarTroubleSolver.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarTroubleSolver.Data.ModelsConfiguration
{
    public class AccidentConfiguration : IEntityTypeConfiguration<Accident>
    {
        public void Configure(EntityTypeBuilder<Accident> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasIndex(a => a.Id).IsUnique();

            builder.HasOne(a => a.Car)
                .WithMany()  // You can specify the navigation property here if you have one
                .HasForeignKey(a => a.CarId);
        }
    }
}
