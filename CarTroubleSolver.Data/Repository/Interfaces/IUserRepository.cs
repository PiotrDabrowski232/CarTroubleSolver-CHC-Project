using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Data.Repository.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public User GetUserByEmail(string email);
    }
}
