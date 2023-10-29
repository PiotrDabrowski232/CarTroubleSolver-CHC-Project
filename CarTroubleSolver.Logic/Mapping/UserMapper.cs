using AutoMapper;
using CarTroubleSolver.Logic.Dto.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheCarMarket.Data.Models;

namespace CarTroubleSolver.Logic.Mapping
{
    public class UserMapper : Profile
    {
        public UserMapper() 
        {
            CreateMap<User, RegisterUserDto>();
            CreateMap<RegisterUserDto, User>();

            CreateMap<User, LogedInUserDto>();
            CreateMap<LogedInUserDto, User>();

            CreateMap<User, UserInformationDto>();
            CreateMap<UserInformationDto, User>();
        }
    }
}
