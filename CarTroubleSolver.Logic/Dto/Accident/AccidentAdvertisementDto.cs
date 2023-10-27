using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Logic.Dto.Cars;
using CarTroubleSolver.Logic.Dto.User;

namespace CarTroubleSolver.Logic.Dto.Accident
{
    public class AccidentAdvertisementDto
    {
        public Guid Id { get; set; }
        public Guid ApplicantUserId { get; set; }
        public UserInformationDto ApplicantUserInfo { get; set; }
        public Guid CarId { get; set; }
        public CarDto CarInfo { get; set; }
        public CollisionSeverity CollisionSeverity { get; set; }
        public string AccidentDescription { get; set; }
    }
}
