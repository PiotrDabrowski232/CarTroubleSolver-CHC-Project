using CarTroubleSolver.Data.Models;
using Microsoft.EntityFrameworkCore;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Data.Database
{
    public class CarTroubleSolverDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Accident> Accidents { get; set; }
        public DbSet<Car> Cars { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarTroubleSolver;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarTroubleSolverDbContext).Assembly);
        }
    }
}
