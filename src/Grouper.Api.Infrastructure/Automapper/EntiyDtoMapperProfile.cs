using AutoMapper;
using Grouper.Api.Data.Entities;
using Grouper.Api.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grouper.Api.Infrastructure.Automapper
{
    public class EntiyDtoMapperProfile: Profile
    {
        public EntiyDtoMapperProfile()
        {
            CreateMap<ApplicationUser, UserDto>();

        }
    }
}
