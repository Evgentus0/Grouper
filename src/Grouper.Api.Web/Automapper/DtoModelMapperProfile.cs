using AutoMapper;
using Grouper.Api.Infrastructure.DTOs;
using Grouper.Api.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Grouper.Api.Web.Automapper
{
    public class DtoModelMapperProfile: Profile
    {
        public DtoModelMapperProfile()
        {
            CreateMap<UserDto, UserModel>().ReverseMap();

            CreateMap<FormDto, FormModel>().ReverseMap();

            CreateMap<GroupDto, GroupModel>().ReverseMap();

            CreateMap<PostDto, PostModel>().ReverseMap();

            CreateMap<CommentDto, CommentModel>().ReverseMap();
        }
    }
}
