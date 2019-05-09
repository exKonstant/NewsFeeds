using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NewsFeeds.API.Models.FeedCollections;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;

namespace NewsFeeds.API.MappingConfig
{
    public class FeedCollectionDtoModelMappingProfile : Profile
    {
        public FeedCollectionDtoModelMappingProfile()
        {
            CreateMap<FeedCollectionDto, FeedCollectionModel>().ReverseMap();
            CreateMap<FeedCollectionAddModel, FeedCollectionDtoForCreate>().ReverseMap();
            CreateMap<FeedCollectionUpdateModel, FeedCollectionDtoForUpdate>().ReverseMap();
        }
    }
}
