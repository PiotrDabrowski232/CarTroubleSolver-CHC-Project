using CarTroubleSolver.Data.Repository;
using CarTroubleSolver.Data.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CarTroubleSolver.Logic.Configuration
{
    public static class RepositoryDependencyConfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection Services)
        {
            Services.AddScoped<IUserRepository, UserRepository>();
            Services.AddScoped<IAccidentRepository, AccidentRepository>();
            Services.AddScoped<ICarRepository, CarRepository>();

            return Services;
        }
    }
}
