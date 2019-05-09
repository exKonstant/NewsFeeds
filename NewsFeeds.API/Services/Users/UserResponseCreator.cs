using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewsFeeds.API.Models.FeedCollections;
using NewsFeeds.API.Models.Users;
using NewsFeeds.BLL.DTOs.FeedCollectionDTOs;
using NewsFeeds.BLL.DTOs.UserDTOs;
using NewsFeeds.BLL.Enums;

namespace NewsFeeds.API.Services.Users
{
    public class UserResponseCreator : IUserResponseCreator
    {
        private readonly IMapper _mapper;

        public UserResponseCreator(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult ResponseForGetAll(IEnumerable<UserDto> userDtos)
        {
            var userModels = _mapper.Map<IEnumerable<UserModel>>(userDtos);
            return new OkObjectResult(userModels);
        }

        public IActionResult ResponseForGet(UserDto userDto)
        {
            if (userDto == null)
            {
                return new NotFoundResult();
            }
            var userModel = _mapper.Map<UserModel>(userDto);
            return new OkObjectResult(userModel);
        }

        public IActionResult ResponseForGetFeedCollections(IEnumerable<FeedCollectionDto> feedCollectionDtos)
        {
            var feedCollectionModels = _mapper.Map<IEnumerable<FeedCollectionModel>>(feedCollectionDtos);
            return new OkObjectResult(feedCollectionModels);
        }

        public IActionResult ResponseForCreate(UserResponse response /*,UserDtoForCreate userDtoForCreate*/)
        {
            switch (response)
            {
                case UserResponse.InvalidFirstName:
                    return new BadRequestObjectResult("Invalid firstname.");
                case UserResponse.InvalidLastName:
                    return new BadRequestObjectResult("Invalid lastname.");
                default:
                    return new OkResult();//CreatedAtRouteResult("GetUser", new { Id = statusCode }, userDtoForCreate);
            }
        }

        public IActionResult ResponseForUpdate(UserResponse response)
        {
            switch (response)
            {
                case UserResponse.InvalidFirstName:
                    return new BadRequestObjectResult("Invalid firstname.");
                case UserResponse.InvalidLastName:
                    return new BadRequestObjectResult("Invalid lastname.");
                case UserResponse.NotExist:
                    return new NotFoundResult();
                default:
                    return new OkResult();
            }
        }
        public IActionResult ResponseForDelete(UserResponse response)
        {
            switch (response)
            {
                case UserResponse.NotExist:
                    return new NotFoundResult();                
                default:
                    return new OkResult();
            }
        }
    }
}

