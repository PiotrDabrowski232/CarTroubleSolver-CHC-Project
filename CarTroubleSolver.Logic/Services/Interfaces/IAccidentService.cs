using CarTroubleSolver.Logic.Dto.Accident;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Services.Interfaces
{
    public interface IAccidentService
    {
        public void AddAccident(AccidentDto accident, string userEmail);
    }
}
