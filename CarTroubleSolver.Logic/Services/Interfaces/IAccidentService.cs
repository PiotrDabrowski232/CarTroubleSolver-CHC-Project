using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Logic.Dto.Accident;

namespace CarTroubleSolver.Logic.Services.Interfaces
{
    public interface IAccidentService
    {
        public void AddAccident<T>(T accidentDto, string userEmail) where T : class;
        public IEnumerable<AccidentAdvertisementDto> GetAllFreeAccidents(string userEmail);
        void HelpInAccident(string email, Guid id);
        public IEnumerable<AccidentHistoryDto> ShowHistoryOfAccidentsAsignee(string email);
        public IEnumerable<AccidentHistoryDto> ShowHistoryOfAccidentsApplicant(string email);
        public AccidentAdvertisementDto GetAccident(Guid id);
        public IEnumerable<AccidentAdvertisementDto> Filter(CollisionSeverity? severity, CarBrand? brand, string userName);
    }
}
