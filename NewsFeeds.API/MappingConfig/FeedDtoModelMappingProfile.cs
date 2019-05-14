using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NewsFeeds.API.Models.Feeds;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.DTOs.UserDTOs;

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
