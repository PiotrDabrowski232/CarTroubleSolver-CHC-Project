using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Data.Repository.Interfaces
{
    public interface IGenericRepository<T>
    {
        public void Add(T entity);
        public T Get(Guid id);
        public void Update(T entity);
        public void Remove(T entity);
        public IEnumerable<T> GetAll();
    }
}
