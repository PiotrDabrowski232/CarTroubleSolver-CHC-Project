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

        public CarTroubleSolverDbContext(DbContextOptions<CarTroubleSolverDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarTroubleSolverDbContext).Assembly);
        }
    }
}
