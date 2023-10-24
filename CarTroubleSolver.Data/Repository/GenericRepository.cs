using CarTroubleSolver.Data.Database;
using CarTroubleSolver.Data.Repository.Interfaces;
using System.Data.Entity;

namespace CarTroubleSolver.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CarTroubleSolverDbContext _context;
        protected GenericRepository(CarTroubleSolverDbContext context)
        {
            _context = context;
        }


        public async Task Add(T entity)
        {
            try
            {
                
                _context.Set<T>().AddAsync(entity);
                
                _context.SaveChanges();
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                var trackedEntities = _context.ChangeTracker.Entries()
    .Where(e => e.State != Microsoft.EntityFrameworkCore.EntityState.Detached)
    .Select(e => e.Entity)
    .ToList();
            }
            catch (Exception ex)
            {

            }


        }

        public T Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public void Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
