﻿using AutoMapper;
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


        public IEnumerable<Accident> GetAllFreeAccidents(string userEmail)
        {
            var loggedUserId = _userRepository.GetUserByEmail(userEmail).Id;

            var accidents = _accidentRepository.GetAll().Where(a => a.ApplicantUserId != loggedUserId && a.AssigneeUserId == null);

            var result = _mapper.Map<IEnumerable<AccidentAdvertisementDto>>(accidents);

            result.ToList().ForEach(a => {
                a.ApplicantUserInfo = _mapper.Map<UserInformationDto>(_userRepository.Get(a.ApplicantUserId));
                a.CarInfo = _mapper.Map<CarDto>(_carRepository.Get(a.CarId));
            });

            return accidents;
        }
    }
}
