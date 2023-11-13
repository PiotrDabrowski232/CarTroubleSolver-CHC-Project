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
        public void Add(CarDto car, string userEmail);
        public void DeleteCarFromUserCollection(CarDto carToDelete, string userEmail);
        public IEnumerable<T> GetUserCars<T>(string userEmail) where T : class;
        public Guid GetCarId(CarDto car, string userEmail);
    }
}
