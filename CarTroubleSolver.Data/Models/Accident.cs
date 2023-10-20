using CarTroubleSolver.Data.Models.Enums;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Data.Models
{
    public class Accident
    {
        public Guid Id { get; set; }
        public Guid ReporterUserId { get; set; }
        public Guid? AssigneeUserId { get; set; }
        public Car Car { get; set; }
        public CollisionSeverity CollisionSeverity { get; set; }
        public string AccidentDescription { get; set; }
    }
}
