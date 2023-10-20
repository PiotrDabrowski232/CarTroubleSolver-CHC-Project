using Microsoft.Extensions.DependencyInjection;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Logic.Services;
using CarTroubleSolver.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using CarTroubleSolver.Logic.Mapping;

namespace CarTroubleSolver.Data.Configuration
{
    public static class ServicesDependencyConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection Services)
        {
            Services.AddScoped<IUserService, UserService>();

            Services.AddDbContext<CarTroubleSolverDbContext>(options =>
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarTroubleSolver;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserMapper>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            Services.AddSingleton(mapper);

            return Services;
        }
    }
}
