using MediatR;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Logic.Functions.UserFunctions.Queries
{
    public class CreateUserQuery : IRequest<Guid>
    {
        public User User { get; set; }
    }
}
