using CarTroubleSolver.Logic.Dto.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Logic.Services.Interfaces
{
    public interface ICarService
    {
        public void Add(CarDto user, string userEmail);
        public void DeleteCarFromUserCollection(CarDto carToDelete, string userEmail);
        public IEnumerable<CarDto> GetUserCars(string userEmail);
    }
}
