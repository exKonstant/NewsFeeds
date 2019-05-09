using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NewsFeeds.API.Models.Users;
using NewsFeeds.BLL.DTOs.UserDTOs;

namespace NewsFeeds.API.MappingConfig
{
    public class UserDtoModelMappingProfile : Profile
    {
        public UserDtoModelMappingProfile()
        {
            CreateMap<UserDto, UserModel>().ReverseMap();
            CreateMap<UserAddOrUpdateModel, UserDtoForCreate>().ReverseMap();
            CreateMap<UserAddOrUpdateModel, UserDto>().ReverseMap();
        }
    }
}
