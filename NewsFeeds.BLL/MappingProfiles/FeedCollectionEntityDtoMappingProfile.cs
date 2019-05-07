using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.DAL.Entities;

namespace NewsFeeds.BLL.MappingProfiles
{
    public class FeedCollectionEntityDtoMappingProfile : Profile
    {
        public FeedCollectionEntityDtoMappingProfile()
        {
            CreateMap<FeedCollectionDto, FeedCollection>().ReverseMap();
            CreateMap<FeedCollectionDtoForCreate, FeedCollection>().ReverseMap();
            CreateMap<FeedCollectionDtoForUpdate, FeedCollection>().ReverseMap();
        }
    }
}
