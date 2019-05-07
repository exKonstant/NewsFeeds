using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NewsFeeds.BLL.DTOs.UserDTOs;
using NewsFeeds.DAL.Entities;

namespace NewsFeeds.BLL.MappingProfiles
{
    public class UserEntityDtoMappingProfile : Profile
    {
        public UserEntityDtoMappingProfile()
        {
            CreateMap<UserDto, User>().ReverseMap();            
            CreateMap<UserDtoForCreate, User>().ReverseMap();
        }
    }
}
