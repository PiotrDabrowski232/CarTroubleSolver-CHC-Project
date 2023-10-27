﻿using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Logic.Dto.Accident;

namespace CarTroubleSolver.Logic.Services.Interfaces
{
    public interface IAccidentService
    {
        public void AddAccident(AccidentDto accident, string userEmail);

        public IEnumerable<AccidentAdvertisementDto> GetAllFreeAccidents(string userEmail);
    }
}
