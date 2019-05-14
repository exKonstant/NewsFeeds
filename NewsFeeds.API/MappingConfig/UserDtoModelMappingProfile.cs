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
            CreateMap<UserAddModel, UserDtoForCreate>().ReverseMap();
            CreateMap<UserAddModel, UserDto>().ReverseMap();
        }
    }
}
