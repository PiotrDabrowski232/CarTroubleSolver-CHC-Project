using CarTroubleSolver.Data.Database;
using CarTroubleSolver.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Data.Repository
{
    public class UserRepository : GenericRepository<User>, IGenericRepository<User>, IUserRepository
    {
        public UserRepository(CarTroubleSolverDbContext context) : base(context)
        {
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void AddCarForUser(Car car, string userEmail)
        {

            
            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            var trackedEntities = _context.ChangeTracker.Entries()
.Where(e => e.State != Microsoft.EntityFrameworkCore.EntityState.Detached)
.Select(e => e.Entity)
.ToList();
            user.Cars.Add(car);
            _context.Users.Update(user);
            _context.SaveChanges();

            
        }
    }
}
