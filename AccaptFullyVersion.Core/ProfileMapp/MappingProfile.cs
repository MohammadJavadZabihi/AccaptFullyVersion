using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.DataLayer.Entites;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.Core.ProfileMapp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserUpdateAccountViewModel, User>();
            CreateMap<User, UserUpdateAccountViewModel>();
        }
    }
}
