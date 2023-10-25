using AutoMapper;
using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Logic.Dto.Accident;

namespace CarTroubleSolver.Logic.Mapping
{
    public class AccidentMapper : Profile
    {
        public AccidentMapper()
        {
            CreateMap<Accident, AccidentDto>();
            CreateMap<AccidentDto, Accident>();
        }
    }
}
