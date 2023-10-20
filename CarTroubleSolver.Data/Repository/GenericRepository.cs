using CarTroubleSolver.Data.Database;
using CarTroubleSolver.Data.Repository.Interfaces;

namespace CarTroubleSolver.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly CarTroubleSolverDbContext _context;
        protected GenericRepository(CarTroubleSolverDbContext context)
        {
            _context = context;
        }


        public async Task<T> Add(T entity)
        {
            _context.Set<T>().AddAsync(entity);

            _context.SaveChanges();

            return entity;
        }

        public T Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
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
