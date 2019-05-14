using AutoMapper;
using NewsFeeds.API.Models.FeedNews;
using NewsFeeds.BLL.Common.FeedNewsBusinessModel;

namespace NewsFeeds.API.MappingConfig
{
    public class FeedNewsMappingProfile : Profile
    {
        public FeedNewsMappingProfile()
        {
            CreateMap<FeedNews, FeedNewsModel>().ReverseMap();
        }
    }
}
