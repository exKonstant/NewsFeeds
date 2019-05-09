using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.FeedCollections;
using NewsFeeds.API.Models.Feeds;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.API.Services.Feeds
{
    public class FeedResponseCreator : IFeedResponseCreator
    {
        private readonly IMapper _mapper;

        public FeedResponseCreator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult ResponseForGetAll(IEnumerable<FeedDto> feedDtos)
        {
            var feedModels = _mapper.Map<IEnumerable<FeedModel>>(feedDtos);
            return new OkObjectResult(feedModels);
        }

        public IActionResult ResponseForGet(FeedDto feedDto)
        {
            if (feedDto == null)
            {
                return new NotFoundResult();
            }
            var feedModel = _mapper.Map<FeedModel>(feedDto);
            return new OkObjectResult(feedModel);
        }

        //public IActionResult ResponseForGetFeeds(IEnumerable<FeedDto> feedDtos)
        //{
        //    var feedModels = _mapper.Map<IEnumerable<FeedModel>>(feedDtos);
        //    return new OkObjectResult(feedModels);
        //}

        public IActionResult ResponseForCreate(FeedResponse response /*,FeedDtoForCreate feedDtoForCreate*/)
        {
            switch (response)
            {
                case FeedResponse.InvalidTitle:
                    return new BadRequestObjectResult("Invalid title.");
                case FeedResponse.InvalidLink:
                    return new BadRequestObjectResult("Invalid link.");
                case FeedResponse.FeedCollectionNotExist:
                    return new BadRequestObjectResult("Feed Collection doesn't exist.");
                case FeedResponse.FeedWithTitleAlreadyExists:
                    return new BadRequestObjectResult("Feed with this title already exists.");
                default:
                    return new OkResult(); //CreatedAtRouteResult("GetFeed", new { Id = statusCode }, FeedDtoForCreate);
            }
        }

        public IActionResult ResponseForUpdate(FeedResponse response)
        {
            switch (response)
            {
                case FeedResponse.FeedNotExist:
                    return new NotFoundResult();
                case FeedResponse.InvalidTitle:
                    return new BadRequestObjectResult("Invalid title.");
                case FeedResponse.InvalidLink:
                    return new BadRequestObjectResult("Invalid link.");
                default:
                    return new OkResult();
            }
        }

        public IActionResult ResponseForDelete(FeedResponse response)
        {
            switch (response)
            {
                case FeedResponse.FeedNotExist:
                    return new NotFoundResult();
                default:
                    return new OkResult();
            }
        }
    }
}
