using CarTroubleSolver.Data.Models.Enums;
using CarTroubleSolver.Logic.Dto.Cars;
using CarTroubleSolver.Logic.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarTroubleSolver.Logic.Dto.Accident
{
    public class AccidentHistoryDto
    {
        public Guid Id { get; set; }
        public Guid ApplicantUserId { get; set; }
        public UserInformationDto ApplicantUserInfo { get; set; }
        public Guid AssigneeUserId { get; set; }
        public Guid CarId { get; set; }
        public CarDto CarInfo { get; set; }
        public CollisionSeverity CollisionSeverity { get; set; }
    }
}
