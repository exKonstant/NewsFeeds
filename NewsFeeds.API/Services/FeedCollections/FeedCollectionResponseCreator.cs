using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.FeedCollections;
using NewsFeeds.API.Models.Feeds;
using NewsFeeds.API.Models.Users;
using NewsFeeds.BLL;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.FeedDTOs;
using NewsFeeds.BLL.DTOs.UserDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.API.Services.FeedCollections
{
    public class FeedCollectionResponseCreator : IFeedCollectionResponseCreator
    {
        private readonly IMapper _mapper;

        public FeedCollectionResponseCreator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult ResponseForGetAll(IEnumerable<FeedCollectionDto> feedCollectionDtos)
        {
            var feedCollectionModels = _mapper.Map<IEnumerable<FeedCollectionModel>>(feedCollectionDtos);
            return new OkObjectResult(feedCollectionModels);
        }

        public IActionResult ResponseForGet(FeedCollectionDto feedCollectionDto)
        {
            if (feedCollectionDto == null)
            {
                return new NotFoundResult();
            }
            var feedCollectionModel = _mapper.Map<FeedCollectionModel>(feedCollectionDto);
            return new OkObjectResult(feedCollectionModel);
        }

        public IActionResult ResponseForCreate(Result result, int userId, FeedCollectionDtoForCreate feedCollectionDtoForCreate)
        {
            switch (result.IsFailure)
            {
                case true:
                    return new BadRequestObjectResult(result.Message);
                default:
                    return new CreatedAtRouteResult("GetFeedCollection", new { feedCollectionId = ((Result<int>)result).Value, userId }, feedCollectionDtoForCreate);
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

