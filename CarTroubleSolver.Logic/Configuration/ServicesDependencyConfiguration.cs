using Microsoft.Extensions.DependencyInjection;
using CarTroubleSolver.Data.Database;
using Microsoft.EntityFrameworkCore;
using CarTroubleSolver.Logic;
using CarTroubleSolver.Logic.Services.Interfaces;
using CarTroubleSolver.Logic.Services;
using AutoMapper;
using CarTroubleSolver.Logic.Mapping;

namespace CarTroubleSolver.Data.Configuration
{
    public static class ServicesDependencyConfiguration
    {
        public static IServiceCollection AddServices(this IServiceCollection Services)
        {
            Services.AddScoped<IUserService, UserService>();
            Services.AddScoped<ICarService, CarService>();
            Services.AddScoped<IAccidentService, AccidentService>();



            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CarMapper>();
                cfg.AddProfile<UserMapper>();
                cfg.AddProfile<AccidentMapper>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            Services.AddSingleton(mapper);

            Services.AddDbContext<CarTroubleSolverDbContext>(options =>
            options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CarTroubleSolver;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"));


            return Services;
        }
    }
}
