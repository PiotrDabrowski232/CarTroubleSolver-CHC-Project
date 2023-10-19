using CarTroubleSolver.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
