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

            CreateMap<Accident, AccidentAdvertisementDto>();
            CreateMap<AccidentAdvertisementDto, Accident>();

            CreateMap<Accident, AccidentHistoryDto>();
            CreateMap<AccidentHistoryDto, Accident>();

            CreateMap<Accident, WebAccidentRequestDto>();
            CreateMap<WebAccidentRequestDto, Accident>()
                .ForMember(dist => dist.CarId, opt => opt.MapFrom(src => src.SelectedCar.Id));
        }
    }
}
