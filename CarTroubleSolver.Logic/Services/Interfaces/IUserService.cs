using CarTroubleSolver.Logic.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Logic.Services.Interfaces
{
    public interface IUserService
    {
        public void Add(RegisterUserDto user);
        public bool VerifyUserInputs(string email, string password);
        public LogedInUserDto GetLoggedInUser(string email);
    }
}
