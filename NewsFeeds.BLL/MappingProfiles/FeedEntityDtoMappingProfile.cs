using AutoMapper;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.DAL.Entities;

namespace NewsFeeds.BLL.MappingProfiles
{
    public class FeedEntityDtoMappingProfile : Profile
    {
        public FeedEntityDtoMappingProfile()
        {
            CreateMap<FeedDto, Feed>().ReverseMap();
            CreateMap<FeedDtoForCreate, Feed>().ReverseMap();
            CreateMap<FeedDtoForUpdate, Feed>().ReverseMap();
        }
    }
}
