using AutoMapper;
using Core.Entites.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TryJobProject.Business.DTOs.Users;

namespace TryJobProject.Business.Mappings.Profiles.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<GetUserDto, User>().ReverseMap();
        }
    }
}
