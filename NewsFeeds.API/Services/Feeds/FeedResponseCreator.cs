using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.FeedCollections;
using NewsFeeds.API.Models.Feeds;
using NewsFeeds.BLL;
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

        public IActionResult ResponseForGetFeedsByFeedCollection(IEnumerable<FeedDto> feedDtos)
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

        public IActionResult ResponseForCreate(Result result, int feedCollectionId, int userId, FeedDtoForCreate feedDtoForCreate)
        {
            switch (result.IsFailure)
            {
                case true:
                    return new BadRequestObjectResult(result.Message);                
                default:
                    return new CreatedAtRouteResult("GetFeed", new { Id = ((Result<int>)result).Value, feedCollectionId, userId }, feedDtoForCreate);
            }
        }

        public IActionResult ResponseForUpdate(Result result)
        {
            switch (result.IsFailure)
            {
                case true:
                    return new BadRequestObjectResult(result.Message);
                default:
                    return new OkResult();
            }
        }

        public IActionResult ResponseForDelete(Result result)
        {
            switch (result.IsFailure)
            {
                case true:
                    return new BadRequestObjectResult(result.Message);
                default:
                    return new OkResult();
            }
        }
    }
}
