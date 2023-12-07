using CarTroubleSolver.Logic.Dto.Accident;

namespace CarTroubleSolver.Web.Models
{
    public class HomeViewModel
    {
        public FilterModel FilterModel { get; set; }
        public List<AccidentAdvertisementDto> Accidents { get; set; }
        public int pageNumber { get; set; } = 1;
    }
}
