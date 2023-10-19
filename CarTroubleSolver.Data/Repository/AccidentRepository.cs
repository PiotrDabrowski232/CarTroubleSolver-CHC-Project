using CarTroubleSolver.Data.Database;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repository.Interfaces;

namespace CarTroubleSolver.Data.Repository
{
    public class AccidentRepository : GenericRepository<Accident>, IGenericRepository<Accident>, IUserRepository
    {
        public AccidentRepository(CarTroubleSolverDbContext context) : base(context)
        {
        }
    }
}
