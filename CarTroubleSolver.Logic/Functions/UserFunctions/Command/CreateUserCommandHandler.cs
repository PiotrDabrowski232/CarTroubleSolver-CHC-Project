using CarTroubleSolver.Data.Repository.Interfaces;
using CarTroubleSolver.Logic.Functions.UserFunctions.Queries;
using MediatR;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Logic.Functions.UserFunctions.Command
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserQuery, Guid>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserQuery request, CancellationToken cancellationToken)
        {
            // Użyj przekazanego użytkownika
            User newUser = request.User;

            // Dodaj nowego użytkownika do repozytorium
            var userId = _userRepository.Add(newUser);

            return userId.Result.Id;
        }
    }
}
