using CarTroubleSolver.Data.Models;

namespace CarTroubleSolver.Data.Repository.Interfaces
{
    public interface IAccidentRepository : IGenericRepository<Accident>
    {
        public void RemoveRange(IEnumerable<Accident> accidents);
    }
}
