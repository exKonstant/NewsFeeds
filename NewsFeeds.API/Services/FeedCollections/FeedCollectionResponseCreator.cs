using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.FeedCollections;
using NewsFeeds.API.Models.Feeds;
using NewsFeeds.API.Models.Users;
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

        public IActionResult ResponseForCreate(FeedCollectionResponse response /*,FeedCollectionDtoForCreate feedCollectionDtoForCreate*/)
        {
            switch (response)
            {
                case FeedCollectionResponse.UserNotExist:
                    return new BadRequestObjectResult("User doesn't exist");
                case FeedCollectionResponse.InvalidName:
                    return new BadRequestObjectResult("Invalid name.");
                case FeedCollectionResponse.FeedCollectionWithThisNameAlreadyExists:
                    return new BadRequestObjectResult("Feed Collection with this name already exists.");
                default:
                    return new OkResult(); //CreatedAtRouteResult("GetUser", new { Id = statusCode }, userDtoForCreate);
            }
        }

        public IActionResult ResponseForUpdate(FeedCollectionResponse response)
        {
            switch (response)
            {
                case FeedCollectionResponse.InvalidName:
                    return new BadRequestObjectResult("Invalid name.");
                case FeedCollectionResponse.FeedCollectionNotExist:
                    return new NotFoundResult();
                default:
                    return new OkResult();
            }
        }

        public IActionResult ResponseForDelete(FeedCollectionResponse response)
        {
            switch (response)
            {
                case FeedCollectionResponse.FeedCollectionNotExist:
                    return new NotFoundResult();
                default:
                    return new OkResult();
            }
        }
    }
}

