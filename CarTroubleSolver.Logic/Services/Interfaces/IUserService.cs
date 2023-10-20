using CarTroubleSolver.Logic.Dto;
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
    }
}
