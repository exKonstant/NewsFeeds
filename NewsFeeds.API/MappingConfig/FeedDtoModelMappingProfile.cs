using AutoMapper;
using NewsFeeds.API.Models.Feeds;
using NewsFeeds.BLL.DTOs.FeedDTOs;

namespace NewsFeeds.API.MappingConfig
{
    public class FeedDtoModelMappingProfile : Profile
    {
        public FeedDtoModelMappingProfile()
        {
            CreateMap<FeedDto, FeedModel>().ReverseMap();
        }
    }
}
