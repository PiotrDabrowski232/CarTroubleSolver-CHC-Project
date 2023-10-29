using CarTroubleSolver.Data.Models.Enums;

namespace CarTroubleSolver.Logic.Dto.Accident
{
    public class AccidentDto
    {
        public Guid ApplicantUserId { get; set; }
        public Guid CarId { get; set; }
        public CollisionSeverity CollisionSeverity { get; set; }
        public string AccidentDescription { get; set; }
    }
}
