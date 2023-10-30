using AutoMapper;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Data.Repository.Interfaces;
using CarTroubleSolver.Logic.Dto.Accident;
using CarTroubleSolver.Logic.Dto.Cars;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;

namespace CarTroubleSolver.Logic.Services
{
    public class AccidentService : IAccidentService
    {
        private readonly IAccidentRepository _accidentRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        public AccidentService(IAccidentRepository accidentRepository, IMapper mapper, IUserRepository userRepository, ICarRepository carRepository)
        {
            _accidentRepository = accidentRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _carRepository = carRepository;
        }

        public void AddAccident(AccidentDto accidentDto, string userEmail)
        {
            var mappedAccident = _mapper.Map<Accident>(accidentDto);
            mappedAccident.Id = Guid.NewGuid();
            mappedAccident.ApplicantUserId =  _userRepository.GetUserByEmail(userEmail).Id;
            _accidentRepository.Add(mappedAccident);
        }


        public IEnumerable<AccidentAdvertisementDto> GetAllFreeAccidents(string userEmail)
        {
            var loggedUserId = _userRepository.GetUserByEmail(userEmail).Id;

            var accidents = _accidentRepository.GetAll().Where(a => a.ApplicantUserId != loggedUserId && a.AssigneeUserId == null);

            var result = _mapper.Map<IEnumerable<AccidentAdvertisementDto>>(accidents);

            result.ToList().ForEach(a => {
                a.ApplicantUserInfo = _mapper.Map<UserInformationDto>(_userRepository.Get(a.ApplicantUserId));
                a.CarInfo = _mapper.Map<CarDto>(_carRepository.Get(a.CarId));
            });

            return result;
        }

        public void HelpInAccident(string email, Guid id)
        {
            var userId = _userRepository.GetUserByEmail(email).Id;

            var accidentToUpdate = _accidentRepository.Get(id);

            accidentToUpdate.AssigneeUserId = userId;

            _accidentRepository.Update(accidentToUpdate);

        }

        public IEnumerable<AccidentHistoryDto> ShowHistoryOfAccidents(string type, string email)
        {
            var userId = _userRepository.GetUserByEmail(email).Id;

            var accidentsResult = _mapper.Map<IEnumerable<AccidentHistoryDto>>(_accidentRepository.GetAll());

            accidentsResult.ToList().ForEach(a => {
                a.ApplicantUserInfo = _mapper.Map<UserInformationDto>(_userRepository.Get(a.ApplicantUserId));
                a.CarInfo = _mapper.Map<CarDto>(_carRepository.Get(a.CarId));
            });

            if (type == "Applicant")
                accidentsResult = accidentsResult.Where(a => a.ApplicantUserId == userId);
            if (type == "Asignee")
                accidentsResult = accidentsResult.Where(a =>  a.AssigneeUserId == userId);


            return accidentsResult;
        }
    }
}
