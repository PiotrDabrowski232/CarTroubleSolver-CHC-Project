using CarTroubleSolver.Data.Database;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repository.Interfaces;

namespace CarTroubleSolver.Data.Repository
{
    public class AccidentRepository : GenericRepository<Accident>, IGenericRepository<Accident>, IAccidentRepository
    {
        public AccidentRepository(CarTroubleSolverDbContext context) : base(context)
        {
        }

        public void RemoveRange(IEnumerable<Accident> accidents)
        {
            _context.Accidents.RemoveRange(accidents);
            _context.SaveChanges();
        }
    }
}
