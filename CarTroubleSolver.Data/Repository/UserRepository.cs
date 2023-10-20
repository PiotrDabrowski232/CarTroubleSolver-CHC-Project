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
    }
}
